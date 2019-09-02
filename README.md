# Dotnet Core Async Task

The task I am trying to do here can be found [here](./Hawkeye_Test.dockx). It had some open-ended path which I have to take some assumptions. 

I am listing my questions below.

### Question

1. By 'each batch', do we mean, one of the files that is dropped in the input folder where the file will have a number of events of either 'Departure' or 'Arrival' type?

2. Can I assume the input file will have valid JSON data?

3. How do we define a failed event? Can I have an example of validation rules?. 

4. If validation rules are changed, should it be applied to all the files in Raw folder or all events from the processed folders, i.e., Exception and Curated? Do we have to delete old processed event file and add new files to the appropriate folder?

5. Is there a pattern of the input filenames?

6. We need to log failed events ID, how do we define the ID of an event?

7. How many events will be there in an input file, in other words, how big an input file can be?

8. When we say, the system will scale to 10 batches per second, do we want the application to be run concurrently by 10 processes?


### Assumptions

1. I am assuming each input file as a batch of events

2. I am assuming valid JSON data

3. I am assuming if an event has less than a certain number of passengers, it is a failed event. 

4. If validation rule changes -- A task will run on all the files in the RAW folder and overwrite process event files.

5. I am assuming the input file will have a ‘.json’ extension

6. I am assuming event id as ‘{eventType}-{timeStamp}’

7. I am assuming input file size will be in KB/MB range

8. I am making a process so that we can run batches concurrently



### The process outline


- I am designing the process keeping concurrency and scaling in mind. We should be able to run a number of batches per second. 

- Each batch (file found from *ReadDir*) will be handed to *InputEventsProcessor* to process. 

- The *ReadDir* will read the input directory and find a file that is not being processed. It will LOCK the file and send the file to the *InputEventsProcessor*. This locking mechanism will restrict multiple *InputEventsProcessor* from working on the same file. For, locking I am going to simply add a '.lock' after the filename. If we planning to have multiple processes to run *InputEventsProcessor*, we can use the processID as a folder and move the input file there. 

- The *InputEventsProcessor* will send the filename to *ReadFile* (an async task runner). The *ReadFile* will read the file and send the file data as a string back to the *InputEventsProcessor*.

- Then the *InputEventsProcessor* will send the file data to two async runners in parallel, i.e., *FileCopier* and *EventSeparator*.

- The *FileCopier* will simply write the content in the given path (RAW folder) as an async task.

- The *EventSeparator* will convert the file data into a list of event objects. It will apply validators on each event based on the result it will use *FileCopier* to write event data into CURATED or EXCEPTION folder. It will also send back success and failure count back to the *InputEventsProcessor*.  The *InputEventsProcessor* will use the returned data for logging.

- Finally, the *InputEventsProcessor* will log the process result and delete the lock file from the input folder.

### Philosophy
I have tried to follow a single responsibility for each class. It makes code more reusable and modular. Also, I have tried to use different data structures, classes and data types whenever I could. 



### Unit test

Added as separate unit test project **myAppTest** which uses *Xunit*.


### Any number of validators 

Used builder design pattern where any number of validator can be added and passed on to *InputEventsProcessor*. Every new Validator have to inherit  abstract *Validator* class. 

### Bonus

I have also added an **input generator** (a node project). Which I have used to generate input files. It will generate 10 input files at a time where each file will have 100 random events.



### Improvements

1. More unit test would be better
2. It could have a better project name than *myApp*
3. I am coding C# after a long time, for sure I have missed recent design and coding paradigm in this domain. 

