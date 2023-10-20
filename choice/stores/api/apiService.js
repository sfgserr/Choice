
const BASE_URI = "https://choiceweb.azurewebsites.net/api";

const get = async (request, body) => {
    const response = await fetch(`${BASE_URI}/${request}`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
        },
        body: body,
    });

    return await response.json();
}

const post = async (request, body) => {
    const response = await fetch(`${BASE_URI}/${request}`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: body,
    });

    return await response.json();
}

const put = async (request, body) => {
    const response = await fetch(`${BASE_URI}/${request}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: body,
    });

    return await response.json();
}

export default {
    get,
    post,
    put,
};