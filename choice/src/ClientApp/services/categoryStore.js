import categoryService from "./categoryService.js";
import blobService from "./blobService.js";

let categories = [];

const getCategories = () => {
    return categories;
}

const downloadImages = async (categories) => {
    await categories.forEach(async c => {
        await blobService.getImage(c.iconUri);
    })
}

const retrieveData = async () => {
    categories = await categoryService.getCategories();
    await downloadImages(categories);
}

export default {
    getCategories,
    retrieveData
}