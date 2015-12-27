namespace ProjectEuler

module Problem001 = 
    let solution = 
        [|for i in 1..999 do if i % 3 = 0 || i % 5 = 0 then yield i|]
        |> Array.sum

    // Answer: 233168

module Problem002 =
    // Create a Fibonacci sequence of elements which are smaller than 4M
    let rec fibb a b  = 
        seq {
            if a + b < 4000000 then 
                yield a + b 
                yield! fibb b (a + b)}

    let sumEvenFibs = 
        fibb 0 1 
        |> Seq.filter (fun x -> x % 2 = 0)
        |> Seq.sum

    // Answer: 4613732

module Problem003 = 
    let isPrime (n :int64) =
        let rec check i = 
            i > n / 2L || (n % i <> 0L && check (i + 1L))
        check 2L

    open System
    let getMaxPrime (n : int64) = 
        {2L..int64(Math.Sqrt(double(n)))}
        |> Seq.filter (fun i -> n % i = 0L && isPrime i)
        |> Seq.max

    // Answer: 6857

module Problem004 =
    let isPalindrom x = 
        let reverse = 
            x.ToString().ToCharArray()
            |> Array.rev 
            |> Array.map (fun i -> i.ToString())
            |> Array.reduce (fun acc elem -> acc + elem)
        reverse = x.ToString()

    let x = 
        {100..999}
        |> Seq.map (fun i -> 
            {100..999}
            |> Seq.map (fun j -> j * i))
        |> Seq.concat
        |> Seq.distinct
        |> Seq.filter isPalindrom
        |> Seq.max

    // Answer: 906609

module Problem005 = 
    let isDivisisable x =
        {2..20}
        |> Seq.map (fun i -> x % i = 0)
        |> Seq.exists (fun i -> i = false)
        |> not 

    open System
    let getValue() = 
        {20..20.. Int32.MaxValue}
        |> Seq.find (fun i -> isDivisisable i = true)

    // Real: 00:00:10.146, CPU: 00:00:10.140, GC gen0: 910, gen1: 2, gen2: 0
    // Answer: 232792560