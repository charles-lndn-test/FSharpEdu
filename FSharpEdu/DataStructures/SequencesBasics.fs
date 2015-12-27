namespace DataStructures.SequencesBasics

module CreateSequence = 
    // Creating sequences 
    let ints = {1..100}

    let ints2 = seq { for i in 1..10 do yield i}
    let ints3 = seq { for i in 1..10 -> i}

    let ints4 = Seq.init 10 (fun i -> i + 1)

    // This creates infinite sequence. In reality sequence in bound by Int32.MaxValue.
    // Also, elements of sequence are calculated on demand, as oppose to array.
    let ints5 = Seq.initInfinite (fun i -> i + 1)

module UnfoldSequence = 
    // We will try to solve FizzBizz problem. We have a sequence on integers: 1, 2, 3, ...
    // We want to swap those which divide by 3 with Fizz, those which divide by 5 with Bizz, and 
    // those which divide by both 3 and 5 with FizzBizz.

    let fizzBizzGenerator = 
        1 
        |> Seq.unfold (fun i ->
            if i >20 then None
            else 
                if i % 3 = 0 && i % 5 = 0 then Some("FizzBizz", i + 1)
                else if i % 3 = 0 then Some("Fizz", i + 1)
                else if i % 5 = 0 then Some("Bizz", i + 1)
                else Some(i.ToString(), i + 1))
        |> Seq.iter (fun s -> printfn "%s" s)

module RecursiveExpressionsSequence = 
    // To generate a sequence using recursion we must makke the whole body of a function a sequence
    // We use yield! to yield the elements returned by the recursion.

    let rec seqWithRecursion i = 
        seq {
            if i < 100 then 
                yield i
                yield! (seqWithRecursion (i + 1))}

// Some other useful functions
// Seq.nth - gets nth element of the sequence. If we populate the sequence using unfold, all elements before nth 
//           have to be evaluated before nth.
// Seq.[first|last] - gets the first or last element in the sequence.

module PairwiseSequence = 
    let arr = [|3; 5; 7;12; 4; 77; 2; 65; 234; 54; 2|]

    let findAllUp arr = 
        arr
        |> Seq.pairwise
        |> Seq.filter (fun (a, b) -> a < b)
        |> Seq.map (fun (a, b) -> b - a)
        |> Seq.sum

module WindowedSequence = 
    let arr = [|3; 5; 7;12; 4; 77; 2; 65; 234; 54; 2|]

    let findPicks arr = 
        arr
        |> Seq.windowed 3
        |> Seq.choose (fun triple ->
            match triple with 
            | [|a; b; c|] when b > a && b > c -> Some b
            | _ -> None)