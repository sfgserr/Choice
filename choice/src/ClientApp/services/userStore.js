import clientService from "./clientService";
import blobService from "./blobService";

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
        await blobService.getImage(user.iconUri);
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