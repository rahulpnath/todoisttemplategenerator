module Tests 

open FsCheck
open Xunit
open TodoistTemplateGenerator
open System
open Swensen.Unquote

[<Fact>]
let ``Parse with Empty Arguments returs default options`` =
    let expected = { templateFile = None; startDate = None}
    let actual = ArgParser.parse []
    test<@ expected = actual @>
