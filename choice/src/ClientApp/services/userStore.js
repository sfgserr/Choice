import clientService from "./clientService";
import blobService from "./blobService";

let user;

const get = () => {
    return user;
}

const login = async (userType) => {
    if (userType == 1) {
        user = await clientService.get();
        await blobService.getImage(user.iconUri);
    }
}

const logout = () => {
    user = null;
}

export default {
    login,
    logout,
    get
}