import React from 'react'
import {
    View,
    Text,
    Switch
} from 'react-native';

const Category = ({ category, selectCategory }) => {
    const [value, setValue] = React.useState(category.track);

    const onValueChanged = () => {
        setValue(prev => {
            selectCategory({
                title: category.title,
                track: !prev,
                id: category.id
            });

            return !prev;
        });
    }

    return (
        <View 
            style={{
            flexDirection: 'row',
            justifyContent: 'space-between',
            paddingTop: 10
        }}>
            <Text 
                style={{
                    fontSize: 17,
                    color: 'black',
                    fontWeight: '400'
                }}>
                {category.title}    
            </Text>
            <Switch
                trackColor={{true: '#2688EB', false: '#001C3D14'}} 
                thumbColor={'white'}
                value={value}
                onValueChange={onValueChanged}/>
        </View>
    );
}

export default Category;