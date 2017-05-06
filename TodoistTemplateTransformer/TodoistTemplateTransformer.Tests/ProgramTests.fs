module ProgramTests

open Xunit
open TodoistTemplateTransformer
open Swensen.Unquote

[<Theory>]
[<InlineData("02-Apr-2017", "02-Apr-2017", 0)>]
[<InlineData("02-Apr-2017", "03-Apr-2017", 1)>]
[<InlineData("02-Apr-2017", "10-Apr-2017", 8)>]
[<InlineData("02-Apr-2017", "02-May-2017", 30)>]
let ``start in days returns correct days`` now newDate expectedDays =
    let expected: Result<float,string> = Success(expectedDays)

    let actual = Main.startInDays now newDate

    test <@ expected = actual @>

[<Theory>]
[<InlineData("02-Apr-2017", "01-Apr-2017")>]
[<InlineData("02-Apr-2017", "01-Apr-2016")>]
[<InlineData("02-Apr-2017", "01-Feb-2017")>]
let ``start in days with new date less than now returns error`` now newDate =
    let expected: Result<float,string> = Failure("New date must be greater than now")

    let actual = Main.startInDays now newDate

    test <@ expected = actual @>


