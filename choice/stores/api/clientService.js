import api from './apiService';

const createClient = async (client) => {
    return await api.post('Client/Create', client);
}

const getClient = async (id) => {
    return await api.get(`Client/${id}/Get`, null);
}

const getClients = async () => {
    return await api.get('Client/Get');
}

const updateClient = async (client) => {
    return await api.put('Client/Update', client);
}

export default {
    createClient,
    getClient,
    getClients,
    updateClient,
}