import React, {ForwardedRef} from 'react';
import {BottomSheetMethods} from '@gorhom/bottom-sheet/lib/typescript/types';
import CustomBottomSheet from './CustomBottomSheet.tsx';
import {CompanyInfo} from '../../types/DomainTypes.ts';
import {
  ActivityIndicator,
  Dimensions,
  Image, Linking, NativeScrollEvent, NativeSyntheticEvent,
  StyleSheet,
  Text,
  View,
} from 'react-native';
import {CompanyService} from '../../services/domain/CompanyService.ts';
import {useDependency} from '../../services/Hooks.ts';
import {BottomSheetView} from '@gorhom/bottom-sheet';
import {FlatList, TouchableOpacity} from 'react-native-gesture-handler';
import {Icon} from '@rneui/themed';
import {GestureStyledButton} from '../buttons/GestureStyledButton.tsx';

type CompanyPageBottomSheetProps = {
  companyId: string
  close: () => void
  navigateToChat: () => void
};

const d = Dimensions.get('screen');

const CompanyPageBottomSheet = React.forwardRef(({companyId, close, navigateToChat}: CompanyPageBottomSheetProps, ref: ForwardedRef<BottomSheetMethods>) => {
  const companyService = useDependency<CompanyService>('CompanyService');

  const [company, setCompany] = React.useState<CompanyInfo | null>(null);

  const [currentIndex, setCurrentIndex] = React.useState(0);

  const minioUrl = `${process.env.MINIO_URL}/app-files`;

  const contacts = React.useMemo(
    () => ({
      ['Instagram']: {
        source: require('../../assets/images/instagram.png'),
        open:async (inst: string) => await Linking.openURL(inst),
      },
      ['Facebook']: {
        source: require('../../assets/images/facebook.png'),
        open:async (inst: string) => await Linking.openURL(inst),
      },
      ['Telegram']: {
        source: require('../../assets/images/tg.png'),
          open: async (tg: string) => await Linking.openURL(tg),
      },
      ['Vk']: {
        source: require('../../assets/images/vk.png'),
        open: async (vk: string) => await Linking.openURL(vk),
      },
      ['Telephone']: {
        source: require('../../assets/images/tel.png'),
        open: async (phoneNumber: string) => await Linking.openURL(`tel:+${phoneNumber}`),
      },
      ['Mail']: {
        source: require('../../assets/images/mail.png'),
        open: async (mail: string) => await Linking.openURL(`mailto:${mail}`),
      },
    }),
    [],
  );

  const onScroll = (event: NativeSyntheticEvent<NativeScrollEvent>) => {
    const totalWidth = event.nativeEvent.layoutMeasurement.width;
    const xPosition = event.nativeEvent.contentOffset.x;
    const newIndex = Math.round(xPosition / totalWidth);

    if (newIndex !== currentIndex) {
      setCurrentIndex(newIndex);
    }
  };

  const onClose = React.useCallback(() => {
    setCompany(null);
    close();
  }, []);

  React.useEffect(() => {
    const getCompany = async () => {
      const response = await companyService.getCompanyOnMap(companyId);

      if (response.content != null) {
        setCompany(response.content);
      }
    }
    getCompany();
  }, [companyId]);

  return (
    <CustomBottomSheet
      ref={ref}
      title={'Компания'}
      close={onClose}>
      <BottomSheetView>
        {company == null ? (
          <View style={styles.indicatorContainer}>
            <ActivityIndicator size={'large'} color={'#2D81E0'} />
          </View>
        ) : (
          <View style={styles.container}>
            <View style={styles.flatListContainer}>
              <FlatList
                data={company.photoUris.filter(s => s != '')}
                onScroll={onScroll}
                renderItem={item => (
                  <View style={styles.imageContainer}>
                    <Image
                      source={{uri: `${minioUrl}/${item.item}`}}
                      style={styles.image}/>
                  </View>
                )}
                horizontal
                pagingEnabled
                showsHorizontalScrollIndicator={false}
              />
              <View style={styles.overlay}>
                <Image
                  style={styles.overlayImage}
                  source={{uri: `${minioUrl}/${company.photoUris[currentIndex]}`}}
                  blurRadius={20}/>
                {company.photoUris
                  .filter(s => s != '')
                  .map((s, i) => (
                    <View
                      style={styles.dotContainer}
                      key={i}>
                      <View style={[styles.dot, {opacity: currentIndex == i ? 1 : 0.22}]}/>
                    </View>
                  ))}
              </View>
            </View>
            <View style={styles.companyCard}>
              <Image
                style={styles.icon}
                source={{uri: `${minioUrl}/${company.iconUri}`}}/>
              <View style={styles.companyDataContainer}>
                <View style={styles.horizontalSpread}>
                  <Text style={styles.boldText}>{company.name}</Text>
                  <View style={styles.horizontalSpread}>
                    <Image
                      source={require('../../assets/images/distance.png')}
                      style={styles.distanceIcon}
                      tintColor={'#99A2AD'}/>
                    <Text style={styles.distance}>{`${company.distance} м от Вас`}</Text>
                  </View>
                </View>
                <Text style={styles.lightText}>{`${company.city}, ${company.street}`}</Text>
              </View>
            </View>
            <View style={styles.reviewContainer}>
              <TouchableOpacity style={styles.reviewCard}>
                <View style={styles.horizontalSpread}>
                  <Icon
                    name={'star'}
                    type={'material'}
                    color={'#E4E839'}
                    size={20}/>
                  <Text style={{...styles.boldText, paddingLeft: 5}}>{company.averageGrade}</Text>
                </View>
                <Text style={styles.lightText}>{`${company.reviewsCount} отзывов`}</Text>
              </TouchableOpacity>
            </View>
            <Text style={{...styles.boldText, paddingTop: 10}}>Деятельность компании</Text>
            <Text style={{...styles.lightText, paddingTop: 10}}>{company.description}</Text>
            <View style={styles.contactsContainer}>
              {company.socialMedias.map((c, i) => {
                const contact = contacts[c.platform];

                return (
                  <TouchableOpacity
                    style={styles.contactButton}
                    onPress={async () => await contact.open(c.url)}
                    key={i}>
                    <Image
                      source={contact.source}
                      style={styles.contactImage}/>
                  </TouchableOpacity>
                )
              })}
              <TouchableOpacity
                style={styles.contactButton}
                onPress={async () => await contacts['Telephone'].open(company.phoneNumber)}>
                <Image
                  source={contacts['Telephone'].source}
                  style={styles.contactImage}/>
              </TouchableOpacity>
              <TouchableOpacity
                style={styles.contactButton}
                onPress={async () => await contacts['Mail'].open(company.email)}>
                <Image
                  source={contacts['Mail'].source}
                  style={styles.contactImage}/>
              </TouchableOpacity>
            </View>
            <GestureStyledButton
              content={'Перейти в чат'}
              top={20}
              bottom={0}
              isDisabled={false}
              pressed={navigateToChat}/>
          </View>
        )}
      </BottomSheetView>
    </CustomBottomSheet>
  );
});

const styles = StyleSheet.create({
  indicatorContainer: {
    flex: 1,
    justifyContent: 'center',
  },
  container: {
    paddingHorizontal: 10,
  },
  flatListContainer: {
    backgroundColor: 'black',
    width: '100%',
    height: d.height * 0.19,
    borderRadius: 15,
    overflow: 'hidden',
  },
  imageContainer: {
    width: d.width * 0.86,
    height: d.height * 0.19,
    borderRadius: 15,
  },
  image: {
    width: '100%',
    height: '100%',
  },
  overlay: {
    borderRadius: 8,
    height: 15,
    position: 'absolute',
    alignSelf: 'center',
    bottom: 5,
    flexDirection: 'row',
    justifyContent: 'center'
  },
  overlayImage: {
    width: '100%',
    height: '100%',
    position: 'absolute',
    resizeMode: 'cover',
    borderRadius: 8
  },
  dotContainer: {
    paddingHorizontal: 4,
    alignSelf: 'center'
  },
  dot: {
    width: 8,
    height: 8,
    borderRadius: 4,
    backgroundColor: 'white'
  },
  companyCard: {
    paddingTop: 10,
    flexDirection: 'row',
  },
  icon: {
    width: 40,
    height: 40,
    borderRadius: 360,
    resizeMode: 'cover',
    alignSelf: 'center'
  },
  companyDataContainer: {
    paddingLeft: 10,
    flex: 1,
    justifyContent: 'space-between',
    alignSelf: 'center'
  },
  horizontalSpread: {
    flexDirection: 'row',
    justifyContent: 'space-between',
  },
  boldText: {
    fontWeight: '700',
    fontSize: 16,
    color: 'black'
  },
  distanceIcon: {
    width: 15,
    height: 15,
    resizeMode: 'cover',
    alignSelf: 'center'
  },
  distance: {
    fontSize: 13,
    fontWeight: '400',
    color: '#99A2AD',
    alignSelf: 'center',
    paddingLeft: 5
  },
  lightText: {
    fontSize: 14,
    fontWeight: '400',
    color: '#99A2AD',
  },
  reviewContainer: {
    paddingTop: 10,
  },
  reviewCard: {
    padding: 10,
    alignSelf: 'flex-start',
    justifyContent: 'center',
    alignItems: 'center',
    borderRadius: 15,
    shadowColor: 'black',
    shadowOffset: {
      width: 10,
      height: 10
    },
    shadowOpacity: 1,
    elevation: 1
  },
  star: {
    width: 25,
    height: 25,
    resizeMode: 'cover'
  },
  contactsContainer: {
    paddingTop: 10,
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center'
  },
  contactButton: {
    justifyContent: 'center',
    alignItems: 'center',
    padding: 10,
    backgroundColor: '#F4F4F4',
    borderWidth: 1,
    borderColor: '#EEEEEE',
    borderRadius: 22
  },
  contactImage: {
    width: 24,
    height: 24,
    resizeMode: 'contain'
  },
})

export default CompanyPageBottomSheet;
