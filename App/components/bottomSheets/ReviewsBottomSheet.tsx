import CustomBottomSheet from './CustomBottomSheet.tsx';
import {BottomSheetFlatList, BottomSheetView} from '@gorhom/bottom-sheet';
import {CompanyInfo, Review} from '../../types/DomainTypes.ts';
import React, {ForwardedRef} from 'react';
import {useDependency} from '../../services/Hooks.ts';
import {ReviewService} from '../../services/domain/ReviewService.ts';
import ReviewItem from '../listItems/ReviewItem.tsx';
import {BottomSheetMethods} from '@gorhom/bottom-sheet/lib/typescript/types';
import {ActivityIndicator, Image, StyleSheet, Text, View} from 'react-native';

const ReviewsBottomSheet = React.forwardRef(({close, company}: {close: () => void, company: CompanyInfo | null}, ref: ForwardedRef<BottomSheetMethods>) => {
  const reviewService = useDependency<ReviewService>('ReviewService');

  const [reviews, setReviews] = React.useState<Review[] | null>(null);

  React.useEffect(() => {
    const getReviews = async () => {
      if (company) {
        const response = await reviewService.getReviews(company.id);

        if (response.content && response.content.length > 0) {
          setReviews(response.content);
        }
      }
    };

    getReviews();
  }, [company]);

  return (
    <CustomBottomSheet
      title={'Отзывы'}
      close={close}
      ref={ref}>
      <BottomSheetView>
        {!reviews || !company ? (
          <View style={{justifyContent: 'center', flex: 1}}>
            <ActivityIndicator size={'large'} color={'#2D81E0'} />
          </View>
          ) : (
          <View style={{gap: 20}}>
            <View style={styles.companyCard}>
              <Image
                style={styles.icon}
                source={{uri: `${process.env.MINIO_URL}/app-files/${company.iconUri}`}}/>
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
            <BottomSheetFlatList
              data={reviews}
              renderItem={item => (
                <ReviewItem review={item.item}/>
              )}/>
          </View>
        )}
      </BottomSheetView>
    </CustomBottomSheet>
  );
});

const styles = StyleSheet.create({
  companyCard: {
    paddingTop: 10,
    flexDirection: 'row',
  },
  icon: {
    width: 40,
    height: 40,
    borderRadius: 360,
    resizeMode: 'cover',
    alignSelf: 'center',
  },
  companyDataContainer: {
    paddingLeft: 10,
    flex: 1,
    justifyContent: 'space-between',
    alignSelf: 'center',
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
    paddingLeft: 5,
  },
  lightText: {
    fontSize: 14,
    fontWeight: '400',
    color: '#99A2AD',
  },
});

export default ReviewsBottomSheet;
