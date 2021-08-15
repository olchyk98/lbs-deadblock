# Development Diary

## Table of contents
  * [Estimations](#estimations)
  * [Log](#log)

<a name="estimations"></a>
## Estimations
### *`DevOps`*: Setup
This section includes the setup of the initial development/testing components,
such as CD/CI service, VCS, and general project setup.

**General project setup** should not take more than 30 minutes of work,
as it's a simple load from an already existing `Dotnet CLI` template. Initialization
of internal debugging tools could take a bit more time (est. 1-2 hours), as the main
playground for staging tests will be located on a local machine (no boilerplate setup provided),
therefore it may take some extra time to assemble a complete system for staging tests.

**VSC** - creating a project on *Github* takes a few seconds, but the general setup
is estimated to take at least 15 minutes. That includes filling README, creating basic
folder structure, setting up hooks, etc.

**CI/CD** - creating a new project on *CircleCI* is also pretty simple, as it
already has a basic boilerplate for doing simple things. The complete setup, however, may
take up to 30 minutes of work. That includes setting up testing and validation pipelines.

### *`Dev`*: Engine
Core functionality for the engine should be implemented in the first
4 hours of development. This includes progressive rendering, custom content pipeline,
maps reader, low-level entity instance, input system, and collision detection.

This is going to be the first step. After that
implementation of the logic part of the game will take the place.

### *`DevOps`*: Staging Tests
Staging tests should be implemented right after the core of the engine is done.
This step includes providing an automatic pipeline for testing new staging builds
and validating that the core functionality for the engine is stable. This step should not
take more than 1 hour of work.

### *`Dev`*: Logic
This step will take the most amount of time. Here's going to be implemented every single
entity, metric, object that can be used in the game process. This also includes adding
new things to the engine, such as progressive sprites positioning and scaling. This also
includes high-level implementations, such as connecting player instances to engine input systems
and giving the player the ability to interact with the map environment. The estimated time for
this step is 6-8 hours.

### *`Dev`*: Content
This step includes gathering the right assets (sounds, sprites, fonts, etc.). This may take up to 12 hours of work to fully gather all the needed things for creating an adequate content section
in the game. In general, the content of the game consists of animated sprites for all the entities,
sprites for all the objects, SFX for all the possible events in the game, and more.

This step also includes creating a custom soundtrack for the game. This may take up to 5 hours
of work. The result shall be included in the `README` for this project.

### *`DevOps`*: Staging Tests
This is a quick process for final testing of the core functionality for the game,
with the newly added features that came from logic implementation.

### *`DevOps`*: Final Testing
Final testing includes the actual test with real people. Once the source code is tested,
and a build is created, this will be tested on a separate machine to find potential
bugs and problems. If there are any known issues with the game, they should be fixed and a new build should be compiled.

The estimated progression time for steps is 2-6 hours. After that, the `README` file should be filled in.
When it's done and a new tag on `VCS` is created, the project is considered **closed** and **done**.

<a name="log"></a>
## Development Log

#### *Jun 12, 2021*
High Concept is done. There's the main idea. Some sketches for design are
ready, so it's time to create a new repo.

#### *Jun 18, 2021*
A new Github repo was created. That's the first day of development.

`247c4e7` - Initial Commit

`0ab6c89` - Setup a new MonoGame Project

#### *Jun 20, 2021*
I'm done with setting up the environment. That took a bit more time, than expected
since I had some internal problems with the server. The time I spent on the fix, won't
be included in the report.

Connecting CircleCI was quick and easy. I may be
editing the configuration file in the future to add more verbose messages, since
it's extremely minimalistic right now.

`40cf3fc` - Setup basic arch for the codebase

`c73e750` - Setup CircleCI project

#### *Jun 23, 2021*
Implemented basic map rendering from map reference files. That's big progress.
That feature includes blocks separation, so it's possible now to create complex
levels. The only thing that is missing to do that is **Content**. That should be added
later.

`e60449d` - Handle multi-layer levels

#### *Jun 26, 2021*
Implemented Input System with the basic keyboard events (player movement, general interaction).
It looks good, so I started writing some story tests for it.

`454ecd9` - Implement player movement

#### *Jul 5, 2021*
Implemented dynamic blocks for the world. The first real implementation based on this is `::TreeSpawner`. Wrote some additional tests for movement and interaction,
as well as for the new tree spawner block and base class for the dynamic blocks.

I also partially implemented progressive positioning for sprites.

`be213db` - Implement dynamic blocks

`a6998aa` - Implement *`bottom-to-point`* rendering mode for sprites

#### *Jul 14, 2021*
Implemented colliders for dynamic blocks.
There's a new world matrix system in place. It fixes a lot of known problems and
has a much better performance when it comes to using dynamic blocks.
Everything is going according to the estimation plan.

`366a66b` - Implement a new world matrix system

`ea9902d` - Implement collider option for dynamic blocks

#### *Jul 16, 2021*
Implemented sprite variants. Now entity models are rotatable. This will be
also used for animation in the future.

`86ecb08` - Implement Sprite Variants

#### *Jul 17, 2021*
Refactoring. Moved rotation code to the entity base class, so that every entity
could now rotate. Presented as a *General Implementation*.

`ce3a971` - Implement rotation on entity level.

#### *Jul 18, 2021*
Implemented player interaction with the world.

`296d381` - Implement env interaction for the player

#### *Jul 26, 2021*
Created GUI setup. Implemented tree score.
Added a new monster spawner and a basic monsters implementation idea.

`a2c15ae` - Add arial font to the project
`925fe68` - Create base for GUI system
`2ab378f` - Implement tree metric
`2da9530` - Implement monster spawner

#### *Jul 28, 2021*
Implemented monster behaviour.

`705da00` - Implement monster behavior and attack cooldown

#### *Jul 29, 2021*
Implemented water block and player metrics for it.
Refactored head bars for the player.

`1a1ae39...@2^` - Implement water and exit paths

#### *Jul 30, 2021*
Low-Level work: enforced a specific screen resolution.
Switched to GPL.

`90e310f` - Enforce a custom screen resolution
`65ad896` - Switch to GPL

#### *Aug 1, 2021*
`30f4be0` - Implement main menu

#### *Aug 4, 2021*
It took much more time than expected since there were multiple ways to implement that.
Provided tests for the class. There's also a signature interface in case I will re-implement
that in the future.

`4f82a3b` - Implement sound system
`2d72ca6` - Implement player orchestrator

#### *Aug 5, 2021*
File read/save implementation provided.
Fixed threading problems. Switched to generic events for input handling.

`a84c310` - Use generic events for input handling
`0e3488a` - Implement score saving to a file

#### *Aug 15, 2021*
The first version of the project is done,
and ready for submission. The build is ready.

