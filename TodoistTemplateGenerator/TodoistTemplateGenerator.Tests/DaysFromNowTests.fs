module DaysFromNowTests

open Xunit
open TodoistTemplateGenerator
open Swensen.Unquote

[<Theory>]
[<InlineData("today", 0 )>]
[<InlineData("tomorrow", 1 )>]
[<InlineData("in 2 days", 2 )>]
[<InlineData("in 20 days", 20 )>]
[<InlineData("in 02 days", 2 )>]
[<InlineData("in 200 days", 200 )>]
[<InlineData("in 99999 days", 99999 )>]
let ``Get days from now for valid string returns expected`` text expectedNumber =
    let expected: Result<int,Error> = Success(expectedNumber)
    let actual = ArgParser.getDaysFromNow text

    test <@ expected = actual @>

[<Theory>]
[<InlineData("todays", "Expected 'in x days, today or tomorrow" )>]
[<InlineData("stomorrow", "Expected 'in x days, today or tomorrow" )>]
[<InlineData("2 days", "Expected 'in x days, today or tomorrow" )>]
[<InlineData("in 20x days", "Expected 'in x days, today or tomorrow" )>]
[<InlineData("in 01 day", "Expected 'in x days, today or tomorrow" )>]
[<InlineData("2", "Expected 'in x days, today or tomorrow" )>]
let ``Get days from now for invalid string returns expected`` text expectedMessage =
    let expected: Result<int,Error> = Failure(InvalidFormat(expectedMessage))
    let actual = ArgParser.getDaysFromNow text

    test <@ expected = actual @>


[<Theory>]
[<InlineData("today", 0 )>]
[<InlineData("tomorrow", 1 )>]
[<InlineData("in 2 days", 2 )>]
[<InlineData("in 20 days", 20 )>]
[<InlineData("in 02 days", 2 )>]
[<InlineData("in 200 days", 200 )>]
[<InlineData("in 99999 days", 99999 )>]
let ``Get days from now string returns expected`` expected (daysFromNow: DaysFromNow) =
    let actual = ArgParser.getDaysFromNowString daysFromNow

    test <@ expected = actual @>