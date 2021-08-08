# High Concept
## Word from the author
This was developed during a period of a few weeks, while I was on vacation. The structural ideas
behind system implementation and integrated patterns are too sophisticated for a school project
since this is not usually done by regular students, as it requires a set of skills. That is only
achievable by working in a professional team, where used design solutions are highly controlled by
highly qualified developers.

With that said, this project is an experiment on the native `XNA` framework. While writing this implementation, I've learned a lot of things about `XNA` and `dotnet` in general. I succeeded
with writing my parsing module for configuration. The project even has its low-level implementation
of *Sprite Pipeline* (partial replacement for *Content Pipeline* by MonoGame).

## What genre will the game be?
This game is going to be developed on top of a custom low-level engine, created especially for this game.
The engine will be developed during the game development. Therefore, the whole codebase,
will be placed in one repo, and will follow the monolith style.

The game itself is going to be a **top view 2D game**, where the main goal is to collect
all the trees while fighting monsters. The game will also have its perks, such as the
need for the player to drink water during the game session, and raising trees.

The game will be extraordinary minimalistic, therefore to keep the simplicity
and the main idea of this project, it will be executed with a special pipeline,
that includes multiple controllers and verifiers for each part of the game. It
will be done to implement the initial idea -- from storyline to architecture design.

Motivation for implementing a **top view 2D game** is that everyone is really
getting tired of boring 2D platformers, therefore this game will make a difference,
and *[probably]* show other game designers that it's possible to create a non-platformer 2D game.

## What other similar games do you take inspiration from?
Probably the most obvious inspiration for this project is *Counter-Strike 2D*, since it's
also top-view, and it has a lot of ideas that are present in this project. Mechanics
and the same top-view perspective was ~~stolen~~ borrowed from a dev-jam game, called
*Glow*.

![Counter-Strike 2D example screen](https://d22blwhp6neszm.cloudfront.net/40/399930/0001screen1.png)

## Game Setting
The idea of the main setting is simple and obvious. It is not something that has a lot of
different detailed elements. Most likely, it's going to be a grass terrain, with a bunch
of obstacles and environmental keys, such as trees and water.

The initial idea for this was to build a small farm, but that would be too dynamic for this
project since we're trying to make it minimalistic here, so a few, distinguishable
*blocks* are enough. The main design was not implemented yet, therefore it's hard to say,
but it won't contain more than 5 environmental keys. The design mock-up must be ready after the
core of the engine is implemented.
