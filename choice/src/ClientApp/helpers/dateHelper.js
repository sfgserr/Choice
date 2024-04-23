const formatDate = (date) => {
    let dateElements = date.split('T');
    let utc = dateElements[0];
    
    let time = dateElements[1].split(':');

    return `${utc} ${Number.parseInt(time[0])+3}:${time[1]}:${time[2].split('.')[0]}`;
}

export default {
    formatDate
}
