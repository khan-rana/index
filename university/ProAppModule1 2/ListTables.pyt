
# from arcgis.gis import GIS
import arcpy


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
        self.label = "list available tables Tool"
        self.description = ""
        self.canRunInBackground = False

    def getParameterInfo(self):
        """Define parameter definitions"""
        params = None
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

    def execute(self, parameters, messages):
        """The source code of the tool."""
        try:
            aprx = arcpy.mp.ArcGISProject("CURRENT")
            defdb_path = aprx.defaultGeodatabase
            arcpy.env.workspace = defdb_path
            # Get and print a list of tables
            tables = arcpy.ListTables()
            feature_classes = arcpy.ListFeatureClasses()
            table_list = [table for table in tables]
            feature_classes_list = [fc for fc in feature_classes]
            arcpy.AddMessage('tables_019A37:'+str(table_list + feature_classes_list))
        except Exception as e:
             arcpy.AddMessage('Error:'+str(e))


