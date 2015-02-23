﻿namespace AkkaFlix

    open System
    open Akka.Actor
    open Akka.FSharp

    module AkkaFlix =

        let users = [| "Jack"; "Jill"; "Tom"; "Jane"; "Steven"; "Jackie" |]
        let assets = [| "The Sting"; "The Italian Job"; "Lock, Stock and Two Smoking Barrels"; "Inside Man"; "Ronin" |]

        let rnd = System.Random()

        let rec loop player =
            player <! { User = users.[rnd.Next(users.Length)] ; Asset = assets.[rnd.Next(assets.Length)] }    
            match Console.ReadKey().Key with
            | ConsoleKey.Escape -> ()
            | _ -> loop player

        [<EntryPoint>]
        let main argv =    
            let system = System.create "akkaflix" (Configuration.load())
            let player = system.ActorOf(Props(typedefof<Player>, Array.empty))
    
            loop player

            system.Shutdown()
            0