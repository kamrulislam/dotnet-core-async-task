const moment = require('moment');
const fs = require('fs')

const PATH= '../myApp/working-directory/input';

const getFlightNumber = () => {
    const flights = ['QA123', 'QA124', 'QA125', 'NZ732', 'NZ733', 'NZ734', 'AAV732', 'AAV733', 'AAV734', 'AAV253', 'AAV254', 'AAV255'];
    return flights[Math.floor(Math.random() * 12)];
}

const getDestination = () => {
    const destinations = ['Brisbane', 'Auckland', 'Melbourne', 'Christchurch', 'Adielade', 'Wellington', 'Parth', 'Sydney'];
    return destinations[Math.floor(Math.random() * 8)];
}

const generateEvent = () => {
    const type = Math.random() < 0.5 ? 'Arrival' : 'Departure';
    return type === 'Arrival' ? JSON.stringify({
        "eventType": "Arrival",
        "timeStamp": moment().subtract(Math.floor(Math.random() * 100), 'day').toISOString(),
        "flight" : getFlightNumber(),
        "delayed" : moment().subtract(Math.floor(Math.random() * 10), 'hour').format('hh:mm'),
        "passengers" : Math.floor(Math.random() * 300) + "" 
    }) :  JSON.stringify({
        "eventType": "Departure",
        "timeStamp": moment().subtract(Math.floor(Math.random() * 100), 'day').toISOString(),
        "flight" : getFlightNumber(),
        "destination": getDestination(),
        "passengers" : Math.floor(Math.random() * 300) + "" 
    });

}

const generateInput = () => {
    const numberOfFiles = 10, numberOfEvents = 100;

    for(let fileNumber = 1; fileNumber <= numberOfFiles; fileNumber++) {
        const eventFile = fs.createWriteStream('./tmp/' + fileNumber + '.json', {
            flags: 'a'  
        });
        eventFile.write('[');
        for(let eventNumber = 1; eventNumber <= numberOfEvents; eventNumber++) {
            eventFile.write(generateEvent()); 
            if (eventNumber != numberOfEvents) {
                eventFile.write(',');        
            }
        }
        eventFile.write(']');
        eventFile.end();
    }
    for(fileNumber = 1; fileNumber <= numberOfFiles; fileNumber++) {
        fs.rename('./tmp/' + fileNumber + '.json', PATH + '/' + fileNumber + '.json', function (err) {
            if (err) throw err;
            console.log('renamed complete');
        });
    }
}

generateInput();