﻿// --------------------------------------------------------------------------------------
// Tests for Http Utilities (mainly HttpUtility.JavaScriptStringEncode now)
// --------------------------------------------------------------------------------------

#if INTERACTIVE
#r "../../bin/FSharp.Data.dll"
#r "../../packages/NUnit.2.6.3/lib/nunit.framework.dll"
#load "../Common/FsUnit.fs"
#else
module FSharp.Data.Tests.UriUtility
#endif

open FsUnit
open NUnit.Framework
open System
open FSharp.Data.Runtime

let uri = new Uri("http://www.myapi.com/%2F?Foo=Bar%2F#frag") |> UriUtils.enableUriSlashes

[<Test>]
[<Platform("Net")>]
let ``ToString contains escaped slashes`` () =
    uri.ToString() |> should equal "http://www.myapi.com/%2F?Foo=Bar%2F#frag"

[<Test>]
[<Platform("Net")>]
let ``AbsoluteUri contains escaped slashes`` () =
    uri.AbsoluteUri |> should equal "http://www.myapi.com/%2F?Foo=Bar%2F#frag"

[<Test>]
[<Platform("Net")>]
let ``Query contains escaped slashes`` () =
    uri.Query |> should equal "?Foo=Bar%2F"

[<Test>]
[<Platform("Net")>]
let ``PathAndQuery contains escaped slashes`` () =
    uri.PathAndQuery |> should equal "/%2F?Foo=Bar%2F"

[<Test>]
[<Platform("Net")>]
let ``AbsolutePath contains escaped slashes`` () =
    uri.AbsolutePath |> should equal "/%2F"

[<Test>]
[<Platform("Net")>]
let ``Uri Fragment is properly set`` () = 
    uri.Fragment |> should equal "#frag"
