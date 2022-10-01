# -*- coding: utf-8 -*-
from datetime import datetime


import datetime
import re
import math
import os
from typing import List, AnyStr, Optional, Dict, Tuple, Union

import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
# from arcgis.gis import GIS
import arcpy
from arcpy import env
import geopandas as gpd


class Toolbox(object):
    def __init__(self):
        """Define the toolbox (the name of the toolbox is the name of the
        .pyt file)."""
        self.label = "Toolbox"
        self.alias = "toolbox"

        # List of tool classes associated with this toolbox
        self.tools = [Tool]


class Tool(object):
    def __init__(self):
        """Define the tool (tool name is the name of the class)."""
        self.label = "Calculation Tool"
        self.description = ""
        self.canRunInBackground = False

    def getParameterInfo(self):
        """Define parameter definitions"""
        input1 = arcpy.Parameter(
            displayName="Names",
            name="in_features",
            datatype="String",
            parameterType="Required",
            direction="Input")

        input2 = arcpy.Parameter(
            displayName="Table Path",
            name="in_features2",
            datatype="String",
            parameterType="Required",
            direction="Input")

        input3 = arcpy.Parameter(
            displayName="shp Path",
            name="in_features3",
            datatype="String",
            parameterType="Required",
            direction="Input")

        input4 = arcpy.Parameter(
            displayName="Criteria Matrix",
            name="in_features4",
            datatype="String",
            parameterType="Required",
            direction="Input")

        params = [input1, input2, input3, input4]
        return params

    def isLicensed(self):
        """Set whether tool is licensed to execute."""
        return True

    def updateParameters(self, parameters):
        """Modify the values and properties of parameters before internal
        validation is performed.  This method is called whenever a parameter
        has been changed."""
        return

    def updateMessages(self, parameters):
        """Modify the messages created by internal validation for each tool
        parameter.  This method is called after internal validation."""
        return

    def plot_chart(self, values_for_graph):
        for X, Y in zip(values_for_graph['x'], values_for_graph['y']):
            plt.plot(X, Y)
        plt.show()

    def execute(self, parameters, messages):
        """The source code of the tool."""
        names_ = parameters[0].valueAsText.split(',')
        site_data_gdbtable_name = parameters[1].valueAsText
        coordinates_gdbtable_name = parameters[2].valueAsText
        df_criteria_raw = parameters[3].valueAsText.strip()

        aprx = arcpy.mp.ArcGISProject("CURRENT")
        defdb_path = aprx.defaultGeodatabase

        df_criteria = convert_str_csv(df_criteria_raw, names_)
        site_data = site_data_load(defdb_path, site_data_gdbtable_name)
        obj = Calculations(names_, df_criteria)
        distance_fnis, values_for_plot = obj.run(site_data)
        new_shp_path = add_calculated_data_to_layer(defdb_path, coordinates_gdbtable_name, distance_fnis)   
        connect_shp_file_to_map(new_shp_path)
        arcpy.AddMessage(distance_fnis)
        self.plot_chart(values_for_plot)


class Calculations:
    def __init__(self, _names: List[str], df_criteria: pd.DataFrame):
        self.names = self.preprocess_names(_names)
        self.value_dict = {} # self.df_criteria
        self.df_criteria = df_criteria

    @classmethod
    def preprocess_names(cls, names: List[str]) -> List[str]:
        ordered_list = []
        all_names = [
            'Previous Land use', 'Site size', 'Geographic location', 'Access',
            'Infrastructure', 'Water risk', 'var7', 'var8', 'var9'
        ]
        for name in all_names:
            if name in names:
                ordered_list.append(name.strip())
        return ordered_list

    def customer_input(self):
        self.df_criteria = pd.DataFrame(columns=self.names, index=self.names)
        for ii, index_name in enumerate(self.names[:-1]):
            for column_name in self.names[ii + 1:]:
                self.df_criteria.loc[index_name, column_name] = self.value_dict[f'{index_name}_{column_name}']

    def calculate_lower_triangle_part(self):
        names = self.df_criteria.columns
        for ii in range(len(names)):
            for jj in range(ii, len(names)):
                if ii == jj:
                    self.df_criteria.loc[names[ii], names[ii]] = 1
                else:
                    self.df_criteria.loc[names[jj], names[ii]] = 1 / self.df_criteria.loc[names[ii], names[jj]]

    def form_pairwise_comparision_table(self):
        self.customer_input()
        self.calculate_lower_triangle_part()

    def prepare_fuzzification(self) -> pd.DataFrame:
        iterables = [self.names, ['one', 'two', 'three', 'four']]

        df_fuzzi_criteria = pd.DataFrame(
            columns=pd.MultiIndex.from_product(iterables, names=["first", "second"]),
            index=pd.Series(self.names, name='index1'),
        )
        return df_fuzzi_criteria

    @classmethod
    def fuzzification_coefficient(cls) -> Dict[str, Dict[str, List[Tuple[float, int]]]]:
        values_dictionary_after = {
            'one': [(1, 1), (2, 1 / 3), (3, 1 / 4), (4, 1 / 5), (5, 1 / 6), (6, 1 / 7), (7, 1 / 8), (8, 1 / 9), (9, 1 / 9)],
            'two': [(1, 1), (2, 2 / 5), (3, 2 / 9), (4, 2 / 9), (5, 2 / 11), (6, 2 / 13), (7, 2 / 15), (8, 2 / 17),
                    (9, 1 / 9)],
            'three': [(1, 1), (2, 2 / 3), (3, 2 / 5), (4, 2 / 7), (5, 2 / 9), (6, 2 / 11), (7, 2 / 13), (8, 2 / 15),
                      (9, 2 / 17)],
            'four': [(1, 1), (2, 1), (3, 1 / 2), (4, 1 / 3), (5, 1 / 4), (6, 1 / 5), (7, 1 / 6), (8, 1 / 7), (9, 1 / 8)]
        }
        values_dictionary_before = {
            'one': [(1, 1), (2, 1), (3, 2), (4, 3), (5, 4), (6, 5), (7, 6), (8, 7), (9, 8)],
            'two': [(1, 1), (2, 3 / 2), (3, 5 / 2), (4, 7 / 2), (5, 9 / 2), (6, 11 / 2), (7, 13 / 2), (8, 15 / 2),
                    (9, 17 / 2)],
            'three': [(1, 1), (2, 5 / 2), (3, 7 / 2), (4, 9 / 2), (5, 11 / 2), (6, 13 / 2), (7, 15 / 2), (8, 17 / 2),
                      (9, 9)],
            'four': [(1, 1), (2, 3), (3, 4), (4, 5), (5, 6), (6, 7), (7, 8), (8, 9), (9, 9)]
        }
        return {'values_dictionary_after': values_dictionary_after, 'values_dictionary_before': values_dictionary_before}

    @classmethod
    def fuzzification_select_value(cls,
            values: List[Tuple[float, int]], current_value: Union[float, int]
    ) -> Optional[Union[float, int]]:
        for metric, value in values:
            if current_value == metric:
                return value
        return None

    def calculate_fuzzification(self, df_fuzzi_criteria: pd.DataFrame) -> pd.DataFrame:
        columns = self.names
        after_diagonal = False
        values_dictionary = self.fuzzification_coefficient()
        values_dictionary_after = values_dictionary['values_dictionary_after']
        values_dictionary_before = values_dictionary['values_dictionary_before']
        for value in columns:
            for inner_value in columns:
                for sub_column in ['one', 'two', 'three', 'four']:
                    if value == inner_value:
                        df_fuzzi_criteria.loc[value, (inner_value, sub_column)] = 1
                        after_diagonal = True
                    else:
                        if after_diagonal:
                            df_fuzzi_criteria.loc[
                                inner_value, (value, sub_column)
                            ] = self.fuzzification_select_value(
                                values_dictionary_after[sub_column],
                                self.df_criteria.loc[value, inner_value]
                            )
                        else:
                            df_fuzzi_criteria.loc[
                                inner_value, (value, sub_column)
                            ] = self.fuzzification_select_value(
                                values_dictionary_before[sub_column],
                                self.df_criteria.loc[inner_value, value]
                            )
            after_diagonal = False
        return df_fuzzi_criteria

    def fuzzification(self) -> pd.DataFrame:
        df_fuzzi_criteria = self.prepare_fuzzification()
        df_fuzzi_criteria = self.calculate_fuzzification(df_fuzzi_criteria)
        return df_fuzzi_criteria

    def calculate_fuzzi_criteria(self) -> pd.DataFrame:
        # self.form_pairwise_comparision_table()
        df_fuzzi_criteria = self.fuzzification()
        return df_fuzzi_criteria

    @classmethod
    def geomean(cls, df: pd.DataFrame) -> pd.Series:
        new_s = pd.Series(index=df.index, dtype=float)
        for index, row in df.iterrows():
            res = 1
            n = 0
            for value in row:
                if value and not math.isnan(value):
                    res *= value
                    n += 1
            if n == 0:
                new_s[index] = None
            else:
                new_s[index] = res**(1/n)
        return new_s

    def calculate_geometric_mean_of_fuzzy_comparision(self, df_fuzzi_criteria: pd.DataFrame) -> pd.DataFrame:
        df_fuzzi_criteria = df_fuzzi_criteria.groupby(axis=1, level='second').agg(self.geomean)
        return df_fuzzi_criteria.loc[:, ['one', 'two', 'three', 'four']]

    @classmethod
    def calculate_ascending(cls, df_fuzzi_criteria: pd.DataFrame) -> pd.Series:
        s_ascending = pd.Series(name='ascending', index=['one', 'two', 'three', 'four'], dtype=float)
        sorted_list = np.sort((1/df_fuzzi_criteria.sum(axis=0)).values)
        s_ascending['one'] = sorted_list[0]
        s_ascending['two'] = sorted_list[1]
        s_ascending['three'] = sorted_list[1]
        s_ascending['four'] = sorted_list[2]
        return s_ascending

    @classmethod
    def calculate_fuzzy_weight_for_each_criteria(
            cls, df_fuzzi_criteria: pd.DataFrame, s_ascending: pd.Series
    ) -> pd.DataFrame:
        df_fuzzi_weight_criteria = df_fuzzi_criteria.multiply(s_ascending.values)
        df_fuzzi_weight_criteria.loc[:, 'weight'] = df_fuzzi_weight_criteria.mean(axis=1)
        df_fuzzi_weight_criteria.loc[:, 'norm_weight'] = df_fuzzi_weight_criteria.loc[:, 'weight'] / df_fuzzi_weight_criteria.loc[:, 'weight'].sum()
        return df_fuzzi_weight_criteria

    @classmethod
    def site_metrics(cls):
        return pd.DataFrame(
            {
                'Previous Land use': [
                    'Site category A',
                    'Site category B',
                    'Site category C',
                    'Site category D'
                ],
                'Site size': ['< 5 acres', '5-10 acres', '> 10 acres', None],
                'Geographic location': [
                    'Low (Inner city) within 2 miles',
                    'Moderate (suburban) 2-5 miles',
                    'High (rural) more than 5 miles',
                    None
                ],
                'Access': [
                    'Low (No site access; more than 5 miles from the motorway; more than 2 miles from the a town centre)',
                    'Moderate (in between)',
                    'High (site access; less than 5 miles from the motorway; less than 2 miles from a town centre)',
                    None
                ],
                'Future Land Use': [
                    'Low sensitivity (Employment; commercial; business parks)',
                    'Moderate sensitivity (Public open space; Residential without private gardens)',
                    'High sensitivity (Residential with private gardens; schools; allotments)',
                    None
                ],
                'Infrastructure': [
                    'Low (Empty land)',
                    'Moderate (Partially built)',
                    'High (Built)',
                    None
                ],
                'Water risk': [
                    'Low (No surface water or no linked surface water within 250 m of the site)',
                    'High (sensitive surface water on or close to the site)',
                    None,
                    None
                ]
            }
        )

    @classmethod
    def site_data_coefficient(cls) -> dict:
        return {
            'Previous Land use_one': [0.1, 10, 10, 40],
            'Previous Land use_two': [0.1, 20, 20, 50],
            'Previous Land use_three': [10, 40, 40, 100],
            'Previous Land use_four': [20, 50, 50, 100],
            'Site size_one': [0.1, 2.5, 7.5],
            'Site size_two': [0.1, 5, 10],
            'Site size_three': [2.5, 7.5, 50],
            'Site size_four': [5, 10, 50],
            'Geographic location_one': [0.1, 1, None],
            'Geographic location_two': [0.1, 2, 5],
            'Geographic location_three': [1, 4, 10],
            'Geographic location_four': [2, 5, 10],
            'Access_one': [0.1, 1, 4],
            'Access_two': [0.1, 2, 5],
            'Access_three':  [1, 4, 20],
            'Access_four': [2, 5, 20],
            'Infrastructure_one': [0.1, 15, 65],
            'Infrastructure_two': [0.1, 25, 75],
            'Infrastructure_three': [15, 65, 100],
            'Infrastructure_four': [25, 75, 100],
            'Water risk_one': [0.1, 20],
            'Water risk_two': [0.1, 50],
            'Water risk_three': [20, 100],
            'Water risk_four': [30, 100],
        }

    def create_empty_site_data(self, df_site: pd.DataFrame) -> pd.DataFrame:
        names = self.names.copy()
        for var in ['var7', 'var8', 'var9']:
            try:
                names.remove(var)
            except ValueError:
                continue
        iterables = [
            names,
            ['one', 'two', 'three', 'four']
        ]

        df_site_empty = pd.DataFrame(
            columns=pd.MultiIndex.from_product(iterables, names=["first", "second"]),
            index=pd.Series(df_site.index, name='index1')
        )
        return df_site_empty

    @classmethod
    def convert_site_values_to_numeric(cls,
            coeffs: list, column: str, current_value: str, df_metric: pd.DataFrame
    ) -> Optional[float]:
        metrics = df_metric[column].values
        for index, value in enumerate(coeffs):
            if current_value.strip() == metrics[index]:
                return value
        return None

    def calculate_site_data_values(self,
            df_site_numeric: pd.DataFrame, df_metric: pd.DataFrame, site_data: pd.DataFrame,
            value_dict: Dict[str, List[float]]
    ) -> pd.DataFrame:
        for index in df_site_numeric.index:
            for first_name in df_site_numeric.columns.get_level_values('first').unique():
                for second_name in df_site_numeric.columns.get_level_values('second').unique():
                    df_site_numeric.loc[index, (first_name, second_name)] = self.convert_site_values_to_numeric(
                        value_dict[f'{first_name}_{second_name}'], first_name, site_data.loc[index, first_name], df_metric
                    )
        return df_site_numeric

    def calculate_numeric_site_data(self, df_site: pd.DataFrame) -> pd.DataFrame:
        df_metric = self.site_metrics()
        df_site = df_site.set_index('Site reference')
        df_site_numeric = self.create_empty_site_data(df_site)
        value_dict = self.site_data_coefficient()
        df_site_numeric = self.calculate_site_data_values(df_site_numeric, df_metric, df_site, value_dict)
        return df_site_numeric

    @classmethod
    def calculate_normalized_fuzzy_decision_matrix_aux(cls) -> dict:
        return {
            'Previous Land use': {
                'tag': 'Negative (min value is required)',
                'one': {'max_col': 'four', 'max_val': 'one', 'min_col': 'one', 'min_val': 'four'},
                'two': {'max_col': 'four', 'max_val': 'two', 'min_col': 'one', 'min_val': 'three'},
                'three': {'max_col': 'four', 'max_val': 'three', 'min_col': 'one', 'min_val': 'two'},
                'four': {'max_col': 'four', 'max_val': 'four', 'min_col': 'one', 'min_val': 'one'},
            },
            'Site size': {
                'tag': 'Positive (max value required)',
                'one': {'max_col': 'four', 'max_val': 'one', 'min_col': 'one', 'min_val': 'four'},
                'two': {'max_col': 'four', 'max_val': 'two', 'min_col': 'one', 'min_val': 'four'},
                'three': {'max_col': 'four', 'max_val': 'three', 'min_col': 'one', 'min_val': 'three'},
                'four': {'max_col': 'four', 'max_val': 'four', 'min_col': 'one', 'min_val': 'one'},
            },
            'Geographic location': {
                'tag': 'Positive (max value required)',
                'one': {'max_col': 'four', 'max_val': 'one', 'min_col': 'four', 'min_val': 'four'},
                'two': {'max_col': 'four', 'max_val': 'two', 'min_col': 'four', 'min_val': 'three'},
                'three': {'max_col': 'four', 'max_val': 'three', 'min_col': 'four', 'min_val': 'two'},
                'four': {'max_col': 'four', 'max_val': 'four', 'min_col': 'four', 'min_val': 'one'},
            },
            'Access': {
                'tag': 'Positive (max value required)',
                'one': {'max_col': 'four', 'max_val': 'one', 'min_col': 'one', 'min_val': 'four'},
                'two': {'max_col': 'four', 'max_val': 'two', 'min_col': 'one', 'min_val': 'three'},
                'three': {'max_col': 'four', 'max_val': 'three', 'min_col': 'one', 'min_val': 'two'},
                'four': {'max_col': 'four', 'max_val': 'four', 'min_col': 'one', 'min_val': 'one'},
            },
            'Infrastructure': {
                'tag': 'Positive (max value required)',
                'one': {'max_col': 'four', 'max_val': 'one', 'min_col': 'one', 'min_val': 'four'},
                'two': {'max_col': 'four', 'max_val': 'two', 'min_col': 'one', 'min_val': 'three'},
                'three': {'max_col': 'four', 'max_val': 'three', 'min_col': 'one', 'min_val': 'two'},
                'four': {'max_col': 'four', 'max_val': 'four', 'min_col': 'one', 'min_val': 'one'},
            },
            'Water risk': {
                'tag': 'Positive (max value required)',
                'one': {'max_col': 'four', 'max_val': 'one', 'min_col': 'one', 'min_val': 'four'},
                'two': {'max_col': 'four', 'max_val': 'two', 'min_col': 'one', 'min_val': 'three'},
                'three': {'max_col': 'four', 'max_val': 'three', 'min_col': 'one', 'min_val': 'two'},
                'four': {'max_col': 'four', 'max_val': 'four', 'min_col': 'one', 'min_val': 'one'},
            }
        }

    @classmethod
    def calculate_one_column_normalized_fuzzy_decision_matrix(cls,
            tag: str, rules: Dict[str, str], df_site_numeric_column: pd.DataFrame
    ) -> pd.Series:
        if tag == 'Positive (max value required)':
            return df_site_numeric_column.loc[:, rules['max_val']] / df_site_numeric_column.loc[:, rules['max_col']].max()
        if tag == 'Negative (min value is required)':
            return df_site_numeric_column.loc[:, rules['min_col']].min() / df_site_numeric_column.loc[:, rules['min_val']]

    def calculate_normalized_fuzzy_decision_matrix(self, df_site_numeric: pd.DataFrame) -> pd.DataFrame:
        neg_pos_dict = self.calculate_normalized_fuzzy_decision_matrix_aux()
        normalized_fuzzy_decision_matrix = pd.DataFrame(
            columns=df_site_numeric.columns, index=pd.Series(df_site_numeric.index, name='index1')
        )
        for first_name in df_site_numeric.columns.get_level_values('first').unique():
            for second_name in df_site_numeric.columns.get_level_values('second').unique():
                normalized_fuzzy_decision_matrix.loc[:, (first_name, second_name)] =\
                    self.calculate_one_column_normalized_fuzzy_decision_matrix(
                        neg_pos_dict[first_name]['tag'],
                        neg_pos_dict[first_name][second_name],
                        df_site_numeric.loc[:, first_name]
                    )
        return normalized_fuzzy_decision_matrix

    @classmethod
    def calculate_normalized_fuzzy_decision_weighted_matrix(cls,
            normalized_fuzzy_decision_matrix: pd.DataFrame, df_fuzzi_weight_criteria: pd.DataFrame
    ) -> pd.DataFrame:
        normalized_fuzzy_decision_weighted_matrix = pd.DataFrame(
            columns=normalized_fuzzy_decision_matrix.columns,
            index=pd.Series(normalized_fuzzy_decision_matrix.index, name='index1')
        )
        for first_name in normalized_fuzzy_decision_matrix.columns.get_level_values('first').unique():
            for second_name in normalized_fuzzy_decision_matrix.columns.get_level_values('second').unique():
                normalized_fuzzy_decision_weighted_matrix.loc[:, (first_name, second_name)] =\
                    normalized_fuzzy_decision_matrix.loc[:, (first_name, second_name)] *\
                    df_fuzzi_weight_criteria.loc[first_name, second_name]
        return normalized_fuzzy_decision_weighted_matrix

    def calculate_FPIS_FNIS(self, normalized_fuzzy_decision_weighted_matrix: pd.DataFrame) -> pd.DataFrame:
        df_fpis_fnis = pd.DataFrame(
            columns=normalized_fuzzy_decision_weighted_matrix.columns, index=pd.Series(['A_plus', 'A_minus'], name='index1')
        )
        neg_pos_dict = self.calculate_normalized_fuzzy_decision_matrix_aux()
        for first_value in normalized_fuzzy_decision_weighted_matrix.columns.get_level_values('first').unique():
            for second_value in normalized_fuzzy_decision_weighted_matrix.columns.get_level_values('second').unique():
                df_fpis_fnis.loc[
                    'A_plus', (first_value, second_value)
                ] = normalized_fuzzy_decision_weighted_matrix.loc[:, (first_value, second_value)].max() \
                    if neg_pos_dict[first_value]['tag'] == 'Positive (max value required)' else \
                    normalized_fuzzy_decision_weighted_matrix.loc[:, (first_value, second_value)].min()
                df_fpis_fnis.loc[
                    'A_minus', (first_value, second_value)
                ] = normalized_fuzzy_decision_weighted_matrix.loc[:, (first_value, second_value)].min() \
                    if neg_pos_dict[first_value]['tag'] == 'Positive (max value required)' else \
                    normalized_fuzzy_decision_weighted_matrix.loc[:, (first_value, second_value)].max()

        return df_fpis_fnis

    def calculate_distance_aux(
            cls,
            df_distance: pd.DataFrame,
            df_fpis_fnis: pd.DataFrame,
            normalized_fuzzy_decision_weighted_matrix: pd.DataFrame,
    ) -> pd.DataFrame:
        for column in normalized_fuzzy_decision_weighted_matrix.columns.get_level_values('first').unique():
            for index_ in normalized_fuzzy_decision_weighted_matrix.index:
                df_distance.loc[index_, column] = (((df_fpis_fnis.loc[(column,)] -
                                                  normalized_fuzzy_decision_weighted_matrix.loc[index_, (column,)])**2).
                                                   sum()/4)**(1/2)
        return df_distance

    def calculate_distance_fpis(
            self, df_fpis_fnis: pd.DataFrame, normalized_fuzzy_decision_weighted_matrix: pd.DataFrame
    ) -> pd.DataFrame:
        df_distance_fpis = pd.DataFrame(
            columns=normalized_fuzzy_decision_weighted_matrix.columns.get_level_values('first').unique(),
            index=normalized_fuzzy_decision_weighted_matrix.index
        )
        return self.calculate_distance_aux(
            df_distance_fpis, df_fpis_fnis.loc['A_plus'], normalized_fuzzy_decision_weighted_matrix
        ).sum(axis=1)

    def calculate_distance_fnis(
            self, df_fpis_fnis: pd.DataFrame, normalized_fuzzy_decision_weighted_matrix: pd.DataFrame
    ) -> pd.DataFrame:
        df_distance_fnis = pd.DataFrame(
            columns=normalized_fuzzy_decision_weighted_matrix.columns.get_level_values('first').unique(),
            index=normalized_fuzzy_decision_weighted_matrix.index
        )
        return self.calculate_distance_aux(
            df_distance_fnis, df_fpis_fnis.loc['A_minus'], normalized_fuzzy_decision_weighted_matrix
        ).sum(axis=1)

    def calculate_closeness_coefficient(self, distance_fpis: pd.DataFrame, distance_fnis: pd.DataFrame):
        df_result = pd.DataFrame(columns=['cci', 'rank'], index=distance_fpis.index)
        df_result['cci'] = distance_fnis / (distance_fpis + distance_fnis)
        df_result['rank'] = df_result['cci'].rank(ascending=False, method='min')
        return df_result

    def set_value_for_graph(self, df_fuzzi_weight_criteria) -> Dict[str, list]:
        x = []
        y = [[0, 1, 1, 0] for _ in range(len(self.names))]
        for index, row in df_fuzzi_weight_criteria.loc[:, ['one', 'two', 'three', 'four']].iterrows():
            x.append(row.values)
        return {'x': x, 'y': y}

    def run(self, site_data) -> Tuple[pd.DataFrame, Dict[str, list]]:
        df = self.calculate_fuzzi_criteria()
        df_new = self.calculate_geometric_mean_of_fuzzy_comparision(df)
        s_ascending = self.calculate_ascending(df_new)
        df_fuzzi_weight_criteria = self.calculate_fuzzy_weight_for_each_criteria(df_new, s_ascending)
        values_for_graph = self.set_value_for_graph(df_fuzzi_weight_criteria)
        df_site_numeric = self.calculate_numeric_site_data(site_data)
        normalized_fuzzy_decision_matrix = self.calculate_normalized_fuzzy_decision_matrix(df_site_numeric)
        normalized_fuzzy_decision_weighted_matrix = \
            self.calculate_normalized_fuzzy_decision_weighted_matrix(normalized_fuzzy_decision_matrix,
                                                                    df_fuzzi_weight_criteria)
        df_fpis_fnis = self.calculate_FPIS_FNIS(normalized_fuzzy_decision_weighted_matrix)
        distance_fpis = self.calculate_distance_fpis(df_fpis_fnis, normalized_fuzzy_decision_weighted_matrix)
        distance_fnis = self.calculate_distance_fnis(df_fpis_fnis, normalized_fuzzy_decision_weighted_matrix)
        return (self.calculate_closeness_coefficient(distance_fpis, distance_fnis).sort_values(by='rank', ascending=False).reset_index().
                rename(columns={'index1': 'Site_refer'}),
                values_for_graph)


def geodb_table_load(table_path: str, fields='*'):
    data = []
    with arcpy.da.SearchCursor(table_path, fields) as cursor:
        for row in cursor:
            data.append(row)
    return data

def site_data_load(defdb_path: str, site_data_table_name: str) -> pd.DataFrame:
    columns = ['id',
        'Site reference', 'Previous Land use', 'Site size numeric',
        'Site size', 'Geographic location', 'Access', 'Infrastructure',
        'Infrastructure_describe', 'Water risk', 'coordinates'
    ]
    table_path = os.path.join(defdb_path, site_data_table_name)
    data = geodb_table_load(table_path)
    return pd.DataFrame(data, columns=columns)


def convert_str_csv(df_criteria_raw: str, names_: List[str]):
    data = []
    for raw_row in df_criteria_raw.split('\n'):
        row = []
        for value in raw_row.split(','):
            row.append(float(value))
        data.append(row)
    return pd.DataFrame(data, columns=names_, index=names_)


def connect_shp_file_to_map(shp_path: str):
    aprx = arcpy.mp.ArcGISProject("CURRENT")
    map = aprx.listMaps()[0]  # assumes data to be added to first map listed
    #shp_path = re.sub('\\\\', '\\', repr(shp_path))
    try:
        map.addDataFromPath(shp_path)
    except Exception as e:
        arcpy.AddMessage(e)

    
def create_new_table_file_name(folder: str, current_name: str):
    # file_name = current_name.rsplit('.', 1)[0]
    new_file_name = current_name+'_result' +'_'.join(str(datetime.datetime.now().timestamp()).split('.'))
    new_file_name = new_file_name + '.shp'
    # folder_parent = folder.rsplit('\\', 1)[0]
    return os.path.join(folder, new_file_name)
      
      
def add_calculated_data_to_layer(defdb_path: str, coordinates_gdbtable_name: str, distance_fnis: pd.DataFrame):

    df = gpd.read_file(defdb_path, driver='FileGDB', layer=coordinates_gdbtable_name)
    new_df = df.merge(distance_fnis, left_on='Site_refer', right_on='Site_refer')
    specific_timestamp = '_'.join(str(datetime.datetime.now().timestamp()).split('.'))
    file_name = f'result_{specific_timestamp}'
    new_shp_path = os.path.join(defdb_path.rsplit('\\', 1)[0], f'{file_name}.shp')
    new_df.to_file(new_shp_path)
    arcpy.FeatureClassToGeodatabase_conversion(new_shp_path, defdb_path)
    return os.path.join(defdb_path, file_name)