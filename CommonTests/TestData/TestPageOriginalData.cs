namespace CommonTests.TestData
{
    internal static class TestPageOriginalData
    {
        public const string Url = "http://stackoverflow.com/questions/23710735/getting-dynamic-columns-in-jqgrid";

        public static readonly string TextTitle = "Getting Dynamic Columns in JQGrid";

        public static readonly string[] TextBlocks =
        {
            "\r\n\r\n        I have a requirement where i need to dynamically increase or decrease Jqgrid column .I have multiple buttons like One,Three,Seven,One Month etc.These buttons denotes days and same i need to add as column names in Jqgrid dynamically.\r\n\r\nHere is the pic of the design..\r\n\r\n\r\n\r\nNow as per the picture ,if i click on current only one column should be visible ,if click on  One Day two columns should be visible and similarly if click on Month thirty columns should be dynamically generated. \r\n\r\nHere is my JQgrid sample code that i am referring..\r\n\r\n                grid.jqGrid({\r\n                datatype: \"local\",\r\n                data: mydata,\r\n                colNames:['Room No', '12', '13','14','15','16'],\r\n                colModel:[\r\n                    {name:'Room No',index:'Room No', width:42, align:'center'},\r\n                    {name:'',index:'', editable:true, align:'center'},\r\n                    {name:'',index:'', editable:true, align:'center'},\r\n                    {name:'',index:'', editable:true,align:'center'},\r\n                    {name:'',index:'', editable:true,align:'center'},\r\n                    {name:'',index:'', sortable:false,align:'center'}\r\n                ],\r\n\r\n\r\nPlease help me..\r\n\r\n    ",
            "\r\n            A little search and I found this topic explaining how to do what you want. And here is a demo adding columns dinamically. Basically, what you have to do is: create a function with the data you want for each of your tabs and insert a click event handler to unload the previous set data and call the function for the specific clicked tab to insert new data in the grid.    \r\n\r\n    "
        };

        public static readonly string[] TextTags =
        {
            "jquery",
            "html",
            "jqgrid"
        };

        public static readonly string[] TextVotes =
        {
            "1",
            "0"
        };
    }
}
