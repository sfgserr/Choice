import clientService from "./clientService";

let user;
let currentUserType;

const get = () => {
    return user;
}

const getUserType = () => {
    return currentUserType;
}

const retrieveData = async (userType) => {
    if (userType == 1) {
        user = await clientService.get();
        currentUserType = 1;
    }
}

const logout = () => {
    user = null;
    currentUserType = 0;
}

export default {
    retrieveData,
    logout,
    get,
    getUserType
}