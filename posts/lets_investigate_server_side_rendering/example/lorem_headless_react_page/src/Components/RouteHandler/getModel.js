import axios from "axios";

export const getModel = async (apiUrl, route = 'index', query) => {

    let url = `${apiUrl}${route}.json`;

    if(query) {
        url += query;
    }

    const data = await axios.get(url);
    return data.data;
}