module Test 
    open System
    open System.Xml.Linq
    open FSharp.Data

    [<Literal>] 
    let SampleXML = """
<authors>
    <author name="Ludwig" surname="Wittgenstein" age="29" />
</authors>"""
    type PersonXml = XmlProvider<SampleXML>

    let GetData() =
        let pa = PersonXml.GetSample()
        pa.Author.Name + " " + pa.Author.Surname + " " + pa.Author.Age.ToString()

