module Tests

open FsCheck
open Xunit
open TodoistTemplateGenerator
open System
open System.IO
open Swensen.Unquote

    [<Theory>]
    [<InlineData("-templateFile TestFile.csv -startDate 10-Apr-2015")>]
    [<InlineData("-startDate 10-Apr-2015 -templateFile TestFile.csv")>]
    let ``Parse with filename and startdate returns expected``(argString: string) =
        let args = argString.Split() |> Array.toList
        let expected = { templateFile = Some("TestFile.csv"); startDate = Some(DateTime.Parse("10-Apr-2015")) }
        let actual = ArgParser.parse args
        test <@ expected = actual @>

    [<Fact>]
    let ``Parse with Empty Arguments returs default options``() =
        let args = []
        let expected = { templateFile = None; startDate = None}
        let actual = ArgParser.parse args
        test <@ expected = actual @>

