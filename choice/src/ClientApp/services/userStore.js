import clientService from "./clientService";

let user;

const get = () => {
    return user;
}

const login = async (userType) => {
    if (userType == 1) {
        user = await clientService.get();
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