module MyList

type MyList<'t> =
    | One of 't
    | Cons of 't * MyList<'t>

type MyString = MyList<char>

let rec iter f lst =
    match lst with
    | One x -> f x
    | Cons (x, rest) ->
        f x
        iter f rest

let rec fold xs acc l  =    
    match l with
    | One x -> xs acc x
    | Cons(x, rest) -> fold xs (xs acc x) rest


let rec concat l1 l2 =
        match l1 with
            | One x -> Cons(x, l2)
            | Cons(x, rest) -> Cons(x, concat rest l2)


let rec concatMyString (str1:MyString) (str2:MyString):MyString =
        concat str1 str2


let len l =
        fold (fun i _ -> i + 1) 0 l


let rec map f lst =
    match lst with
    | One x -> One (f x) 
    | Cons (x, rest) -> Cons((f x), (map f rest))




let bubbleSort xs =
        let rec loop xs = 
            match xs with
            | Cons(first,One second) ->
                if first > second    
                then Cons(second, One first)
                else Cons(first, One second )
            | Cons(first, Cons(second, rest)) -> 
                if first > second    
                then Cons(second, loop (Cons(first, rest)))
                else Cons(first, loop (Cons(second, rest)))
            | One x -> One x 
    
        let rec tailRec xs a =
            if a = len xs
            then xs
            else tailRec (loop xs) (a + 1)

        tailRec xs 0


let listToMyList l =
        let rec go lst myLst =
            match lst with
            | [] -> myLst
            | x :: rest -> go rest (Cons(x, myLst))

        if List.isEmpty l
        then
            failwith "Expected not empty list"
        else
            go (List.rev l).Tail (One l.[l.Length - 1])

let myListToList myLst =
        fold (fun list x -> list @ [x]) [] myLst

let stringToMyString str:MyString =
        listToMyList (Seq.toList str)

let myStringToString (str:MyString) =
        myListToList str |> List.toArray |> System.String |> string
