import React from "react"
import {
    View,
    FlatList
} from 'react-native';
import categoryStore from "../services/categoryStore";

const CompanyCategory = () => {
    const categories = categoryStore.getCategories();

    return (
        <View>
            <FlatList
                data={categories}
                renderItem={({item}) => {
                    return <View>
                        
                        </View>
                }}/>
        </View>
    )
}

export default CompanyCategory;