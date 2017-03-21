namespace TodoistTemplateGenerator

open System
open System.IO

type TemplateFilePath = Path
type TemplateStartDate = DateTime

type Arg = {
    templateFile: TemplateFilePath;
    startDate: TemplateStartDate;
}

