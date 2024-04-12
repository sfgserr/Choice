import userStore from "./userStore";

const apiKey = '893c71be7dfe47b897e4f622951e11af';

const getCoords = async () => {
    let user = userStore.get();
    let text = `${user}`;

    return await fetch('https://api.geoapify.com/v1/geocode/search?text=');
}