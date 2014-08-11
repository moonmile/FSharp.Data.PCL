open Test

[<EntryPoint>]
let main _ = 
    // getTestData() |> Async.Ignore |> Async.RunSynchronously

    let o = new Test.Class1()
    
    let name = Test.getName()
    printfn "%A" name
    0
