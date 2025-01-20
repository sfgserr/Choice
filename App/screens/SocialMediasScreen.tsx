import {Dimensions, FlatList, StyleSheet, Text, View} from 'react-native';
import React from 'react';
import SocialMediaItem from '../components/listItems/SocialMediaItem.tsx';
import SocialMediaModal from '../components/modals/SocialMediaModal.tsx';
import {StyledButton} from '../components/buttons/StyledButton.tsx';

const d = Dimensions.get('screen');

export default function SocialMediasScreen({next}: {next: (socialMediaUrls: string[]) => void}) {
  const [urls, seturls] = React.useState<string[]>(['', '', '', '']);

  const [isToggled, setIsToggled] = React.useState(false);
  const [currentIndex, setCurrentIndex] = React.useState(0);

  const handlePress = () => {
    setIsToggled(prev => !prev);
  }

  const onPress = (index: number, val: boolean) => {
    if (val) {
      setCurrentIndex(index);
      handlePress();
    }
    else {
      seturls(prev => {
        prev[index] = '';
        return [...prev];
      })
    }
  }

  const socialMedias = [
    {
      title: 'Instagram',
      icon: require('../assets/images/instagram.png'),
      uri: urls[0],
      onPress: (val: boolean) => onPress(0, val)
    },
    {
      title: 'Facebook',
      icon: require('../assets/images/facebook.png'),
      uri: urls[1],
      onPress: (val: boolean) => onPress(1, val)
    },
    {
      title: 'ВК',
      icon: require('../assets/images/vk.png'),
      uri: urls[2],
      onPress: (val: boolean) => onPress(2, val)
    },
    {
      title: 'Telegram',
      icon: require('../assets/images/tg.png'),
      uri: urls[3],
      onPress: (val: boolean) => onPress(3, val)
    },
  ];

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Социальные сети</Text>
      <FlatList
        data={socialMedias}
        style={styles.flatList}
        renderItem={item => (
          <SocialMediaItem item={item.item}/>
        )}/>
      <View style={styles.buttonContainer}>
        <StyledButton
          content={'Далее'}
          top={0}
          bottom={0}
          isDisabled={urls.every(s => s == '')}
          pressed={() => next(urls)}/>
      </View>
      <SocialMediaModal
        isToggled={isToggled}
        handlePress={handlePress}
        title={socialMedias[currentIndex].title}
        onChange={(val) => seturls(prev => {
          prev[currentIndex] = val;
          return [...prev];
        })}/>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'white',
    paddingTop: 10
  },
  title: {
    fontWeight: '700',
    fontSize: 17,
    color: 'black',
  },
  flatList: {
    paddingTop: 10
  },
  buttonContainer: {
    bottom: 20,
    position: 'absolute',
    alignSelf: 'center',
    width: d.width * 0.9,
  },
});
