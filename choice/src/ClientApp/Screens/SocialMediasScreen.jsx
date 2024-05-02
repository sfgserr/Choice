import React from 'react';
import {
    View,
    Text,
    Image,
    Switch,
    TouchableOpacity,
    Modal,
    Dimensions,
    TextInput
} from 'react-native'
import styles from '../Styles';
import { Icon } from 'react-native-elements';
import urlValidator from '../validators/urlValidator';

const SocialMediasScreen  = ({handleState}) => {
    const [modalVisible, setModalVisible] = React.useState(false);

    const { width, height } = Dimensions.get('screen');

    const [urlName, setUrlName] = React.useState('');

    const [currentUrl, setCurrentUrl] = React.useState('');

    const [instagramUrl, setInstagramUrl] = React.useState('');
    const [facebookUrl, setFacebookUrl] = React.useState('');
    const [vkUrl, setVkUrl] = React.useState('');
    const [tgUrl, setTgUrl] = React.useState('');

    const [disable, setDisable] = React.useState(true);

    const updateState = (state) => {
        setDisable(state.every(u => u == ''));
    }

    return (
        <View
            style={{
                backgroundColor: 'white',
                paddingTop: 10,
                flex: 1,
                paddingHorizontal: 20,
            }}>
            <Modal
                visible={modalVisible}
                transparent={true}
                animationType='slide'>
                <View
                    style={{
                        height,
                        width,
                        backgroundColor: 'rgba(0,0,0,0.5)',
                    }}>
                    <View
                        style={{
                            backgroundColor: 'white',
                            width: '90%',
                            borderRadius: 20,
                            alignSelf: 'center',
                            position: 'absolute',
                            bottom: height/14
                        }}>
                        <View 
                            style={{
                                flex: 1,
                                flexDirection: 'column'
                            }}>
                            <View 
                                style={{
                                    flexDirection: 'row',
                                    justifyContent: 'space-between',
                                    paddingTop: 20,
                                    paddingHorizontal: 20
                                }}>
                                <Text
                                    style={{
                                        fontSize: 24,
                                        fontWeight: '600',
                                        color: 'black'
                                    }}>
                                    {`Ссылка на ваш ${urlName}`}
                                </Text>
                                <TouchableOpacity
                                    onPress={() => {
                                        setModalVisible(false);
                                    }}
                                    style={{
                                        borderRadius: 360,
                                        backgroundColor: '#eff1f2',
                                        alignSelf: 'flex-start',
                                        padding: 2
                                    }}>
                                    <Icon 
                                        name='close'
                                        type='material'
                                        size={27}
                                        color='#818C99'/>
                                </TouchableOpacity>
                            </View>
                            <View style={{paddingHorizontal: 20, paddingTop: 10}}>
                                <View
                                    style={styles.textInput}>
                                    <TextInput 
                                        style={styles.textInputFont}
                                        value={currentUrl}
                                        onChangeText={(text) => setCurrentUrl(text)}/>
                                </View>
                            </View>
                            <View
                                style={{
                                    justifyContent: 'center',
                                }}>
                                <View
                                    style={{
                                        paddingTop: 10,
                                        paddingBottom: 10,
                                        paddingHorizontal: 20
                                    }}>
                                    <TouchableOpacity 
                                        style={[styles.button, {borderRadius: 10}]}
                                        onPress={() => {
                                            if (urlName == 'Instagram') {
                                                if (urlValidator.validateInstagramUrl(currentUrl)) {
                                                    setInstagramUrl(currentUrl);
                                                    
                                                    updateState([
                                                        currentUrl,
                                                        facebookUrl,
                                                        vkUrl,
                                                        tgUrl
                                                    ]);
                                                }
                                            }

                                            if (urlName == 'Facebook') {
                                                if (urlValidator.validateFacebookUrl(currentUrl)) {
                                                    setFacebookUrl(currentUrl);

                                                    updateState([
                                                        instagramUrl,
                                                        currentUrl,
                                                        vkUrl,
                                                        tgUrl
                                                    ]);
                                                }
                                            }

                                            if (urlName == 'ВК') {
                                                if (urlValidator.validateVkUrl(currentUrl)) {
                                                    setVkUrl(currentUrl);

                                                    updateState([
                                                        instagramUrl,
                                                        facebookUrl,
                                                        currentUrl,
                                                        tgUrl
                                                    ]);
                                                }
                                            }

                                            if (urlName == 'Telegram') {
                                                if (urlValidator.validateTgUrl(currentUrl)) {
                                                    setTgUrl(currentUrl);

                                                    updateState([
                                                        instagramUrl,
                                                        facebookUrl,
                                                        vkUrl,
                                                        currentUrl
                                                    ]);
                                                }
                                            }

                                            setModalVisible(false);
                                        }}>
                                        <Text style={styles.buttonText}>Сохранить</Text>
                                    </TouchableOpacity>
                                </View>
                            </View>
                        </View>
                    </View>
                </View>
            </Modal>
            <Text
                style={{
                    color: 'black',
                    fontWeight: '700',
                    fontSize: 17
                }}>
                Социальные сети    
            </Text>
            <View style={{paddingTop: 20}}>
                <View
                    style={{
                        flexDirection: 'row',
                        justifyContent: 'space-between',
                    }}>
                    <View style={{flexDirection: 'row'}}>
                        <Image 
                            source={require('../resources/instagram.png')}
                            style={{
                                width: 20,
                                height: 20
                            }}/>
                        <Text
                            style={{
                                fontWeight: '400',
                                fontSize: 15,
                                color: 'black',
                                paddingLeft: 10
                            }}>
                            Instagram
                        </Text>
                    </View>
                    <Switch
                        trackColor={{true: '#2688EB', false: '#001C3D14'}} 
                        thumbColor={'white'}
                        value={instagramUrl != ''}
                        onValueChange={(value) => {
                            if (value) {
                                setUrlName('Instagram');
                                setCurrentUrl(instagramUrl);
                                setModalVisible(true);
                            }
                            else {
                                setInstagramUrl('');

                                updateState([
                                    '',
                                    facebookUrl,
                                    vkUrl,
                                    tgUrl
                                ]);
                            }
                        }}/>        
                </View>
                <View
                    style={{
                        flexDirection: 'row',
                        justifyContent: 'space-between',
                        paddingTop: 10,
                    }}>
                    <View style={{flexDirection: 'row'}}>
                        <Image 
                            source={require('../resources/facebook.png')}
                            style={{
                                width: 20,
                                height: 20,
                                resizeMode: 'contain'
                            }}
                            />
                        <Text
                            style={{
                                fontWeight: '400',
                                fontSize: 15,
                                color: 'black',
                                paddingLeft: 10
                            }}>
                            Facebook
                        </Text>
                    </View>
                    <Switch
                        trackColor={{true: '#2688EB', false: '#001C3D14'}} 
                        thumbColor={'white'}
                        value={facebookUrl != ''} 
                        onValueChange={(value) => {
                            if (value) {
                                setUrlName('Facebook');
                                setCurrentUrl(facebookUrl);
                                setModalVisible(true);
                            }
                            else {
                                setFacebookUrl('');

                                updateState([
                                    instagramUrl,
                                    '',
                                    vkUrl,
                                    tgUrl
                                ]);
                            }
                        }}/>        
                </View>
                <View
                    style={{
                        flexDirection: 'row',
                        justifyContent: 'space-between',
                        paddingTop: 10
                    }}>
                    <View style={{flexDirection: 'row'}}>
                        <Image 
                            source={require('../resources/vk.png')}
                            style={{
                                width: 20,
                                height: 20,
                                resizeMode: 'contain'
                            }}/>
                        <Text
                            style={{
                                fontWeight: '400',
                                fontSize: 15,
                                color: 'black',
                                paddingLeft: 10
                            }}>
                            ВК
                        </Text>
                    </View>
                    <Switch
                        trackColor={{true: '#2688EB', false: '#001C3D14'}} 
                        thumbColor={'white'}
                        value={vkUrl != ''} 
                        onValueChange={(value) => {
                            if (value) {
                                setUrlName('ВК');
                                setCurrentUrl(vkUrl);
                                setModalVisible(true);
                            }
                            else {
                                setVkUrl('');

                                updateState([
                                    instagramUrl,
                                    facebookUrl,
                                    '',
                                    tgUrl
                                ]);
                            }
                        }}/>        
                </View>
                <View
                    style={{
                        flexDirection: 'row',
                        justifyContent: 'space-between',
                        paddingTop: 10
                    }}>
                    <View style={{flexDirection: 'row'}}>
                        <Image 
                            source={require('../resources/telegram.png')}
                            style={{
                                width: 20,
                                height: 20,
                                resizeMode: 'contain'
                            }}/>
                        <Text
                            style={{
                                fontWeight: '400',
                                fontSize: 15,
                                color: 'black',
                                paddingLeft: 10
                            }}>
                            Telegram
                        </Text>
                    </View>
                    <Switch
                        trackColor={{true: '#2688EB', false: '#001C3D14'}} 
                        thumbColor={'white'}
                        value={tgUrl != ''} 
                        onValueChange={(value) => {
                            if (value) {
                                setUrlName('Telegram');
                                setCurrentUrl(tgUrl);
                                setModalVisible(true);
                            }
                            else {
                                setTgUrl('');

                                updateState([
                                    instagramUrl,
                                    facebookUrl,
                                    vkUrl,
                                    ''
                                ]);
                            }
                        }}/>        
                </View>
            </View>
            <View 
                style={{
                    flex: 1, 
                    justifyContent: 'flex-end', 
                    paddingBottom: 10
                }}>
                <TouchableOpacity 
                    style={[styles.button, { backgroundColor: disable ? '#ABCDf3' : '#2D81E0' }]}
                    disabled={disable}
                    onPress={!disable && handleState}>
                    <Text style={styles.buttonText}>
                        Далее
                    </Text>
                </TouchableOpacity>
            </View>
        </View>
    )
}

export default SocialMediasScreen;