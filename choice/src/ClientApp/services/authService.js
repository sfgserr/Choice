

const loginByEmail = async (email, password) => {
    return await fetch(`http://10.0.2.2/api/Auth/Login?email=${email}&password=${password}`, {
        method: 'POST',
        headers: {
            Accept: 'application/json',
            'Content-Type': 'application/json'
        }
    })
    .then(response => response.json())
    .then(json => {
        console.log(json);
        return json;
    })
    .catch(error => {
        console.log(error);
    });
}

export default {
    loginByEmail
}