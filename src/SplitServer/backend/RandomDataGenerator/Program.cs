// See https://aka.ms/new-console-template for more information
using GrapeCity.ActiveReports;
using RandomDataGenerator;
using System.Reflection;
using System.Xml;

Console.WriteLine("Hello, World!");
var gen = new ResultGenerator().GetRandomData();
Console.WriteLine(gen);

var ret = new PageReport();
var ass = Assembly.GetExecutingAssembly();
var ss = $"第5版结果报表.rdlx";
using var stream = ass.GetManifestResourceStream($"{ass.GetName().Name}.{ss}");
if (stream != null)
{
    using var streamReader = new StreamReader(stream);
    using var reader = new StringReader(streamReader.ReadToEnd());
    ret.Load(reader);
    ret.Report.DataSources.First().ConnectionProperties.ConnectString = "{\r\n  \"Name\": \"结果统计报表\",";
}

