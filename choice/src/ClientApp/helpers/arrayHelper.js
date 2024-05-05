
const project = (array, predicate) => {
    let projectedArray = [];

    array.forEach(item => {
        projectedArray.push(predicate(item));
    })
    
    return projectedArray;
}

const where = (array, predicate) => {
    let sortedArray = [];

    array.forEach(item => {
        if (predicate(item)) {
            sortedArray.push(item);
        }
    });

    return sortedArray;
}

export default {
    project,
    where
}