const formatDate = (date) => {
    let dateElements = date.split('T');
    let utc = dateElements[0];
    
    let time = dateElements[1].split(':');

    return `${utc} ${Number.parseInt(time[0])+3}:${time[1]}:${time[2].split('.')[0]}`;
}

const getTimeFromString = (timeString) => {
    let dateElements = timeString.split('T');

    let time = dateElements[1].split(':');

    return `${time[0]}:${time[1]}`;
}

const convertDateToString = (date) => {
    let year = date.getFullYear();
    let month = date.getUTCMonth()+1;
    let day = date.getDate();

    return `${day < 10 ? '0'+day : day}.${month < 10 ? '0'+month : month}.${year}`;
}

const convertTimeToString = (time) => {
    let minutes = time.getMinutes();
    let hours = time.getHours();

    return `${hours < 10 ? '0'+hours : hours}:${minutes < 10 ? '0'+minutes : minutes}`;
}

const convertDateToJson = (date, time) => {
    let newDate = date.split('.');

    return `${newDate[2]}-${newDate[1]}-${newDate[0]}T${time}:00.000Z`;
}

export default {
    formatDate,
    convertDateToString,
    convertTimeToString,
    convertDateToJson,
    getTimeFromString
}
