# ZEngine (Zechy's Engine)
The goal of this project is to create a custom game engine, entirely written in the C# language and using the .NET framework, incorporating
modern techniques and practices. For linking game systems, dependency injection is used, and for primary logging,
native extensions from Microsoft are utilized. It also focuses on native multi-threading support.

_The main motivation for creating this engine was primarily the desire to delve deeper into the intricacies of game engine development, and
to learn about the things that are hidden behind engine creation. The engine does not claim to be professional, and any potential use
is at one's own risk._

## Architecture
The foundation of game objects is based on components, where basic ones, like transformation, are provided by the engine. Users have
the freedom to expand the logic of game objects with their own components.

### Communication
The engine offers two methods of communication between objects.

#### Messages
Game objects can communicate with each other through messages. These can be sent to any object that implements the
`IMessageReceiver` interface. A unified approach to processing this communication has been created in the form of `MessageHandler`, which can be embedded in objects.

With `SendMessage`, one can target specific methods offered by objects. Specific recipients must be marked with
the `MessageReceiverAttribute`.

#### Events
Events are managed through the `EventMediator`, and are intended for cases when the source or recipient of the message is unknown. Any method can register
with the mediator, which is accessible via DI, to receive messages of a certain type.

Messages are objects that inherit the `IEventMessage` interface.

### Systems
The main building blocks for the engine's operation are modular systems. These are independent parts that manage specific
logic. For example, the management of game objects, rendering, or inputs. These systems are connected to the engine via DI or
can be additionally added to the `GameManager`. Using the `IGameSystem` interface, one can create their own, additional systems.

#### Game Object System
This system is responsible for the creation and destruction of game objects. Developers can access these methods through
the `ObjectManager`.

#### Input System
The system for processing user inputs. At the moment, we are only implementing the keyboard and mouse within the Windows environment.
For interactions in one's own code, the `InputManager` is available, through which one can subscribe to events received from
devices.

### GameBuilder
An object that helps create a basic environment and dependencies from which the `GameManager` can then be further developed.

### GameManager
The main class responsible for the game loop and managing all systems. The GameManager is registered in the game's DI as a singleton,
which should be loaded and initiated in the player's implementation.

## Project Layout
The structure of the project is as follows:

* **Solutions Items** contains files like this readme or `.gitignore`.
* **Sources** Contains the engine's source codes.
    1. **Architecture** Contains libraries defining the engine's basic architecture.
    2. **Game Core** The core responsible for the basic game loop process.
    3. **Game Systems** Systems responsible for the engine's individual functional units.
* **Tests** Contains tests for the engine.