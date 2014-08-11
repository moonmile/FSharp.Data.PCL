module Test

#if FALSE
open FSharp.Data
open FSharp.Data.HttpRequestHeaders

type Stocks = CsvProvider<"http://ichart.finance.yahoo.com/table.csv?s=MSFT">

type RSS = XmlProvider<"http://tomasp.net/blog/rss.aspx">

type GitHub = JsonProvider<"https://api.github.com/repos/fsharp/FSharp.Data/issues">

let getTestData() = async {
    do! Http.AsyncRequest("https://accounts.coursera.org/api/v1/login",
                          headers = [ Origin "https://accounts.coursera.org"
                                      "X-CSRFToken", "something"
                                      Referer "https://accounts.coursera.org/signin" ], 
                          body = FormValues [ "email", "a"; "password", "b" ],
                          cookies = [ "csrftoken", "something" ],
                          silentHttpErrors = true)
        |> Async.Ignore
    do! Http.AsyncRequest("http://m.nationalrail.co.uk/pj/ldbboard/dep/vic",
                          headers = [ UserAgent "Mozilla/5.0 (compatible; MSIE 10.0; Windows Phone 8.0; Trident/6.0; IEMobile/10.0; ARM; Touch; NOKIA; Lumia 920)" ])
        |> Async.Ignore
    let! stocks = Stocks.AsyncGetSample()
    let! rss = RSS.AsyncGetSample()
    let! issues = async {
        try
            // doesn't work on Win8 (#548)
            return! GitHub.AsyncGetSamples()
        with _ -> 
            return [| |]
    }
    let! indicator = WorldBankDataProvider<Asynchronous=true>.GetDataContext().Countries.``United Kingdom``.Indicators.``School enrollment, tertiary (% gross)``
    let freebaseResults = 
        try
            // doesn't work on Win8 (#549)
            [ for reader in Seq.truncate 5 <| FreebaseData.GetDataContext().``Arts and Entertainment``.Books.``Audio book readers`` -> sprintf "%s" reader.Name ]
        with _ ->
            []
    let result = 
        [ 
          [ for row in Seq.truncate 5 stocks.Rows -> sprintf "HLOC: (%A, %A, %A, %A)" row.High row.Low row.Open row.Close ]
          [ for item in Seq.truncate 5 rss.Channel.Items -> item.Title ]
          [ for issue in Seq.truncate 5 issues -> sprintf "#%d %s" issue.Number issue.Title ]
          [ for year, value in Seq.truncate 5 indicator -> sprintf "%d %f" year value ]
          freebaseResults
        ]
        |> List.collect id
        |> String.concat "\n"
    return result 
}

let getTestDataAsTask() = 
    getTestData() |> Async.StartAsTask
#endif

    open System
    open FSharp.Data

    [<Literal>] 
    let SampleXML = """
<authors>
    <author name="Ludwig" surname="Wittgenstein" age="29" />
</authors>"""

    type Parson = FSharp.Data.XmlProvider<SampleXML>
    type Class1()  = 
        member val Target = Parson.GetSample() with get, set

        member this.GetName() =
            this.Target.Author.Name
        member this.GetData() =
            let name = this.Target.Author.Name
            let age  = this.Target.Author.Age
            System.String.Format("name:{0} age:{1}", name, age )            


//    let goTest() =
//        let pa = Parson.Parse( """<authors><author name="" aurname="" age="10" /></authors>""" )
//        pa.Author.Name


    let getName() =
        let mutable name = Parson.GetSample().Author.Name
        name <- name + ""
        name
    let getData() =
        let au = Parson.GetSample().Author
        System.String.Format("name:{0} age:{1}", au.Name, au.Age ) 

        
