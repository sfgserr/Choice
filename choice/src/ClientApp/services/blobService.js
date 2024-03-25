
const getImage = async (fileName) => {
    return await fetch(`https://localhost:7291/api/objects/${fileName}`, {
        method: 'GET',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        }
    })
    .then(response => response.json())
    .catch(error => {
        console.log(error);
    });
}

export default {
    getImage
}