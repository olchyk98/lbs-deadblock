# Initial Planning
This document is about the initial planning for the project. It contains technical
specifications and ideas about the implementation. Some patterns that will
be used in the development of this project in the future maybe not specified here.

## Design
### Assets
Assets for this project were taken from different free game assets
online distributors, such as [Craftpix](https://craftpix.net) and [OpenGameArt](https://opengameart.org/). This was done just to be able to fill the game with some kind of content. Any of the
used assets can (and should) be replaced if this project ever comes to production. Since everything is built on small sprites and a custom engine, it's easy to manage and verify how the
assets are used in the game.

### Style
The chosen style for the assets fully fits the chosen game design style. Pixel 2D assets with
top-view entities gave the best graphical results. It fits the gameplay context, and therefore a decision was made, to use only pixel assets for the game.

## Architecture Design
### Ground
The project is built on top of `dotnet3.1` with `XNA MonoGame` framework. In some cases `dotnet SDK 5` was used to make the assertion and run some testing utils for the project. Despite that, the project itself can be fully built just using `dotnet3` with installed `NuGet` as a package manager. This project had some constraints for the implementation, therefore it was not possible to install
any other 3rd party libraries, such as JSON parsers and Instance handlers. The only present package in the project is `MonoGame`.

### Patterns / Ideas
The codebase was build using an incremental approach, with reversed testing. It means that
every new class should somehow be related to the existing "branch" in the codebase, and follow
the same style as its ancestor. A potential problem with this approach may be accidentally creating a monolith, where it is not possible anymore to efficiently extend and test the code. This problem was solved in the project, by separating code into multiple sub-modules, where it would
be possible to independently test each package. Unfortunately, it's not possible
to fully separate each module, but the existing modules are loosely coupled. It means that it's not going to be hard to replace parts from other modules, in case of a need.

The code is optimized with `interfaces` and `abstract` classes. It has an exact structure,
and it follows some of the main programming principles. Each ***functional*** class
has its own `interface` that makes it easy to debug and replace wrongly implemented parts when it's needed.

### Engine
The engine for this project was developed in 1 full week. That includes custom assets pipeline implementation, core physics, input system, session implementation, and SFX player implementation. The contents of the game are build **on top** of the engine, and therefore it's easy to quickly debug and fix potential bugs in the game. This approach may create a problem with detaching the top layer of the game from the engine. It can be a real problem in large, production-ready products, but in this project, I could benefit from that. Since it wasn't necessary to split the project into multiple parts for multi-contributor development, I was able to boost up the development speed.

### Content Implementation
The engine presents its own **Assets Pipeline** solution, which is fully capable of
handling complex 2D sprites from within a text configuration file, represented in a custom 
format. It supports re-scaling, re-positioning, active instances, readers, and more. Unlike `MonoGame Content Pipeline`, it does not have GUI, instead, it's fully manageable from a single configuration file. The implemented Assets Pipeline is built **on top** of the **Content Pipeline Pipeline**, therefore, it is still linked to the `MonoGame Core`, which is a requirement for this project.

## Tools
### VCS (Version Control System)
***`Github`*** is one of the most popular `Git` services. I usually keep all of my projects
here, therefore it was chosen for the development. Each significant change (such as a complete task or fix) would be placed on a separate branch (previously detached from `c:origin/master`). That way it was easy to remove and manage tasks that are no longer needed. It also gave an extra step for pushing to the master. Every bit of code pushed to master was double-checked during the review process. That and `CD/CI` testing saved a lot of time by detecting small bugs right away. Each commit follows the *"Conventional Commits"* specification for a better understanding of what's going on
in the development process. A lot of commits have even their summary, which contains 
a detailed instructions about what was done, and what approaches were used to solve the problem.

### Testing (CD/CI)
Testing and Linting for this project were implemented using a cloud platform called ***`CircleCI`***. Each commit on ***`Github`*** would fire a custom workflow and execute a few pre-defined processes. That includes testing, test build, and format checking. Status for each build is visible
in the repo. It's easy to track errors and find out, why something has crashed.

### `story-observer`
This is a custom tool developed by me, used to track progress. It was mainly used
in this project to generate User Stories and Bug Reports. That's a great way to
handle error stack traces and logs. At the same time, it was also used to track progress for other
parts of the development process, such as design and content.

## Building
Since the developed engine takes advantage of the custom implementation of the assets pipeline,
it can get tricky when it comes to building the project. To avoid any problems **all**
present contents for the game would be built in the `COPY` mode. Meaning, all
content files would stay in the root folder, so that the game binary would
have direct access to them. In theory, it would be awesome to fix this problem
and make the custom implementation of the assets pipeline to put assets into the game build,
but that would take some extra time for the development. In the context of this project,
it's not needed.

## Integration with C++
This project is taking advantage of minor libraries written in C++.
As this language is known for flexible memory control, it was used to
write prototypes for the advanced AI interaction, and dynamic environment
changes. Despite the fact, that great progress was made, I didn't
have enough time to finish this up, therefore monsters take advantage
of a basic AI movement algorithm.

Using C++ libraries in this project would also lead to some problems with
the building. Since sometimes it's needed to build libraries for each platform,
it can get tricky and dirty. This was one of the potential problems, but
it's fixable. The original solution for this was to build everything in the cloud and save
it as artifacts, but that could also lead to some problems.

