open Test

[<EntryPoint>]
let main argv = 
    // error: 追加情報:メソッドが見つかりません: 'Void System.IO.StringReader..ctor(System.String)'
    printfn "%A" (Test.getData())
    0

