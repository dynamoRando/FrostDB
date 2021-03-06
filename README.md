# No Longer Maintained
I've decided that this codebase is no longer useful to maintain, and I am leaving it up here for reference purposes.

# FrostDB
A **proof of concept** implementation of an idea I had called a [Cooperative Database System](https://github.com/dynamoRando/CooperativeDatabaseSystems) or CDBS. This is an older paper I had with some of the inital concepts. I've worked to try and simplify these concepts for a minimum viable product in Frost.

# License
This project has been created with a GNU General Public License v3.0. See [file](https://github.com/dynamoRando/FrostDB/blob/master/LICENSE) for details. Some portions of the code have been incorporated from other Open Source Software projects. Those projects have had their README and respective licenses included in the repository where applicable.

# What Is This About
FrostDB is meant to be a friction-less as possible drop-in replacement for application developers to enable users of their applications to own their data. It takes a similiar approach as the SOLID project (https://solidproject.org/) but instead of leveraging open protocol standards, it is more database centric.

For application developers, they will still need to define their database schema, and they will need to define a **data contract** which is an agreement between them and their users on what data will be stored, and where. The data contract is a public document that is sent to all users that contains the entire database schema and the permissions for all parts of the schema.

# How Frost Works (Or At Least, How I Imagine It To)
When you create a database, you define the tables and columns in it. In Frost, you also define a "data contract" between you and your users. A data contract contains the entire schema of your database system, the permissions for every object, and more importantly, where the data in your database will be stored (either locally on your Frost copy, or on your users copy of the Frost database.)

You also define "participants" or users for your database. When you add a participant, you add an IP Address and Port Number of a user who is also hosting FrostDB.

Participants get a copy of the database contract, and if they accept the data contract, Frost creates on their local host a *partial database* containing the tables that they are agreeing to participate with. 

When data is entered into Frost via an INSERT statement (much like SQL) there is an additional keyword "FOR PARTICIPANT" where you direct that row being inserted to be saved at a particular user's copy of Frost. For participant data, in the database author's copy, a reference is stored to the row that was sent.

When data is retrieved via a SELECT statement, Frost will aggregate all the appropriate requests from all the participants, if applicable, for the data plus any local information in the local copy of the database. 

Frost was built with a SQL-centric view in mind, and I wanted to keep most of that familiarity for people used to SQL to be the same in Frost. Underneath the hood, however, this is all just JSON and some TCP/IP code. Again, this project is a **proof of concept**.

In the future, I may reboot this project to be more NoSQL-like.

For some illustrations around the concepts, see the paper at the start of this README.

For participants in a database, they should have full authority over their data that is hosted locally on their Frost instance; including later denying the database author access to the data, or even deleting it, leading to orphaned references in the main database.

# The Components
## FrostConsole
A Frost instance can be run from FrostConsole.exe. In the application startup path, it expect two folders: "dbs" for where it will store Frost databases and partial databases, and "contracts" where it will store a copy of all contracts that have been accepted.

Files that end in ".frost" are the instance's authored copy of the database. Files that end in ".frostPart" are partial copies of a database that instance has agreed to participate in.

When starting up it will ask if you wish to start an instance, or if you'd like to configure your Frost instance. In the application startup path there will be a "frost.config" created that saves this information. It will prompt for an IP Address to host Frost on as well as a Data Port Number and a Console Port Number.

A data port number is meant for Frost to Frost instance communication.

A console port number is meant for the FrostForm.exe software to connect to so that you can administer your Frost instance.

## Other Items
There are various other test projects I've included in the solution as well as general Frost Libaries.

### FrostBlazeServer
This was an experimental look at Microsoft's Blazor applications in an attempt to make the FrostForm component web-based and cross platform. This is a server side Blazor implementation meant for administration of your FrostDB instance.

### FrostCommon
A library meant to hold common objects that a Frost instance and affliated components will use for communication purposes.

### FrostDB
The root libary that is Frost.

### FrostDbClient
The client library meant to be used for developers to connect to Frost and perform various actions.

# Random Thoughts
There are various projects that are working on trying to solve ownership of user data, or at least tangentially. Off the top of my head I am aware of -

* [SOLID](https://solidproject.org/)
* [Ocean Protocol](https://oceanprotocol.com/)
* [Blockstack](https://blockstack.org/)
* [IndieWeb](https://indieweb.org/)
* [Data Transfer Project](https://datatransferproject.dev/)
* [GUNDB](https://github.com/amark/gun/blob/master/README.md#what-is-gun)
* [Personal Information Management Systems](https://edps.europa.eu/data-protection/our-work/subjects/personal-information-management-system_en)

I think this is great, and I make no bones that others may eventually have the right idea. For me, this is my personal take on trying to solve the problem.

I personally try to avoid leveraging blockchain based solutions. I am not anti-blockchain, I just believe there are only certain use cases for it. Some thoughts [here](https://www.youtube.com/watch?v=15RTC22Z2xI) and [here](https://www.linkedin.com/pulse/blockchain-emperor-has-clothes-dr-stephen-norman/).

I do think that data portability and data commonality are important concepts that, if I have enough time, will also get around to implementing in Frost. Commonaility is an idea that is that Microsoft is looking at with their "[Common Data Model](https://docs.microsoft.com/en-us/common-data-model/)." Data portability and owning your data are both ideas are are rolled into the solutions being discussed by IndieWeb, SOLID, and the Data Transfer Project, mentioned previously.

# Disclaimers
Once someone has read access to your data, even once, all bets are off on what they've done with that data. I acknowledge this in the paper mentioned at the start of this README along with some other architecture disclaimers. Other projects like the SOLID project also agree that there is little control over this fact, short of **re-engineering the entire app and data chain**. (See the [FAQ](https://solidproject.org/faqs#pod) from SOLID). You could probably build an ecosystem to account for this, but that would be a rather large project. For me personally, this project is just my "programmer voice" contribution to other solutions to try and steer IT into really thinking about these problems and driving towards solutions. I'd love to work on that bigger problem later on, if ever.

This project is a proof of concept project and is very much still under development. Nothing will likely work right completely if you use it. **Don't** use this in any production environment what so ever. This project is a **proof of concept.** It's meant to illustrate the idea about giving users ownership of their data; but has not been engineered at all with security in mind.

I'm not really a software developer. I'm learning this as I go along with this project. The "databases" in this project are really just large JSON files. The TCP/IP code in this project I stole from [MSDN](https://docs.microsoft.com/en-us/dotnet/framework/network-programming/asynchronous-server-socket-example) because I didn't know what I was doing. I should probably scrutinize more the layout of my project, but it's hard when you're only dedicating 4 hours a week (or so) and you just want to get the concepts implemented. I'm trying to learn to apply SOLID programming principles as I go.

I'm writing this software in my spare time. I'll welcome any feedback.

This project is being constructed in Visual Studio Commmunity 2019 Preview. It is Windows centric, but not meant to be only Windows software.

I would love for this project to be cross platform. It is currently being written in .NET Core 3.1, where applicable. It is in C#, though I had debated awhile back rebooting the project in Rust. Feel free to do so if you want!

This project is being written by a SQL Server developer, and as such, is very SQL-centric. In the future I may either add on or reboot this project to be more NoSQL-like.

I have had zero training in lower level engineering concepts, such as TCP/IP communications, or writing a lexer, or binary trees. I'm faking it till I make it.

Originally this project was named "Whiskey Tango Foxtrot" because I didn't know what I was doing. It was then shortened to "Foxtrot" but to avoid confusion with the Firefox browser I randomly chose "Frost" as a replacement name.

# Referenced Projects
* [C5](https://github.com/sestoft/C5)
* [Collectathon](https://github.com/Entomy/LibLangly)
