namespace DataStructures.ArraysBasics

module CreatingArrays = 
    // Creating an array.

    // Use ";" instead of ",".
    let primes = [|2; 3; 5; 7; 11|]

    // Use range:
    let numbers = [|1000..1020|]

    let numbers2 = [|100..2..120|]

    let numbers3 = [|100.0..3.3..120.0|]

    // Use loops. In this case we have to yield the value.
    let evens = [|for i in 1..100 do if i % 2 = 0 then yield i|]

    // A little more complicated example. Create a list of last days in each month.
    open System
    let lastDaysYear year = 
        [|
            for i in 1..12 do 
                let firstDay = DateTime(year, i, 1)
                let lastDay = firstDay.AddDays(float(DateTime.DaysInMonth(year, i) - 1))
                yield lastDay.Date
        |]

    // Do the same using Array.init
    open System
    let lastDaysYear2 year = 
        Array.init 12 (fun i -> 
            let month = i + 1;
            let firstDay = DateTime(year, month, 1)
            let lastDay = firstDay.AddDays(float(DateTime.DaysInMonth(year, month) - 1))
            lastDay
        )

    // Convert IEnumerable<T> into array.
    open System
    let files = System.IO.Directory.EnumerateFiles(@"C:\Temp")
                |> Array.ofSeq

    // There is also Array.zeroCreate but it's rarely used anyway.

module AccessingArray = 
    // First let's create a function returning last days of each month.
    open System
    let LastDays year = 
        [|
            for i in 1..12 do 
                let firstDay = DateTime(year, i, 1)
                let lastDay = firstDay.AddDays(float(DateTime.DaysInMonth(year, i) - 1))
                yield lastDay.Day
        |]
    
    // In order to access any of them, we use dot notation.
    let daysOf2015 = LastDays 2015

    let lastFeb = daysOf2015.[1]

    // To change the value
    daysOf2015.[1] <- 7

    // Although array is immutable, elements within the array can be changed. Usually updating 
    // the value of the array is a bad idea, and we should ask ourselves whether it wasn't possible 
    // to create the array with correct values in the first place, e.g. by using if-else inside 
    // for-do instruction.

module MapArray =   
    let numbers = [|1..99|]

    let squares = 
        numbers 
        |> Array.map (fun n -> n * n)

    let fruits = [|"apple"; "orange"; "banana"|]

    open System
    let InitCap str = 
        if String.IsNullOrEmpty str then 
            str 
        else 
            str.[0].ToString().ToUpperInvariant() + str.[1..]

    let initCapFruits = 
        fruits 
        |> Array.map (fun f -> InitCap f)

    // Alternatively, this can be shortened by using 
    
    let initCapFruits2 = 
        fruits 
        |> Array.map InitCap

module MapiArray = 
    // Does exactly the same, but also provides us with a zero-based index
    // of the element of the array, that is currently being processed.

    let numbs = [|1..20|]

    let numbsDiv3 = 
        numbs 
        |> Array.mapi (fun i x -> 
            if x % 3 = 0 then 
                sprintf "Elem at %i div by 3, val %i" i x
            else 
                sprintf "Elem does not div")

// Array.iter allows you to iterate through the collection, but does not return anything.
// It is designed to interact through side-effects, so in pure functional language, this 
// function woldn't have right to exist.

// Array.filter allows to filers the array.

module ChoooseArray = 
    // From the list of URL, try downloading each. Then discard those which failed, and display the final result.
    // First, implementation using map-filter
    open System.Net
    let getRequests() = 
        let urls = 
            [|
                "http://www.google.com"
                "http://www.theguardian.com"
                "invalid-url"
            |]
        use wc = new WebClient()
        urls
        |> Array.map (fun url ->
            try 
                wc.DownloadString(url) |> Some
            with 
            | _ -> None)
        |> Array.filter (fun s -> s.IsSome)
        |> Array.map (fun s -> s.Value)
        |> Array.iter (fun s -> printfn "Content: %s" (s.Trim().Substring(0, 100)))

    // Alternative implementation using Arra.choose (which is a substitution for boilerplate code map-filter)
    let getRequests2() = 
        let urls = 
            [|
                "http://www.google.com"
                "http://www.theguardian.com"
                "invalid-url"
            |]
        use wc = new WebClient()
        urls
        |> Array.choose (fun url ->
            try 
                wc.DownloadString(url) |> Some
            with 
            | _ -> None)
        |> Array.iter (fun s -> printfn "Content: %s" (s.Trim().Substring(0, 100)))

module SumSortArray = 
    let numbs = [|1..10|]
    let numbPairs = [| for i in 1..10 do yield (i, i * i)|]

    let sumNumbs = 
        numbs
        |> Array.sum

    let sumBySquares = 
        numbPairs
        |> Array.sumBy (fun (a, b) -> b)

    // The same way sorting can be applied.

module ReduceArray =
    let arr = [|"aaa"; "bbb"; "ccc"|]

    let makeCsv (arr : array<string>) = 
        arr
        |> Array.reduce (fun acc elem -> sprintf "%s, %s" acc elem)

module FoldArray = 
    let arr = [|"aaa"; "bbb"; "ccc"|]

    let makeWrappedCsv (separator : string) (arr : array<string>) = 
        arr
        |> Array.fold (fun acc elem -> sprintf "%s%s%s" acc elem separator) separator

module ZipArray =
    let arr1 = [|1..4|]
    let arr2 = [|11..14|]

    let mult = 
        Array.zip arr1 arr2
        |> Array.map (fun (x1, x2) -> x1 * x2)