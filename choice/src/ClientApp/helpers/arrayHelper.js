
const project = (array, predicate) => {
    let projectedArray = [];

    array.forEach(item => {
        projectedArray.push(predicate(item));
    })
    
    return projectedArray;
}

export default {
    project
}