import {useEffect, useState} from 'react';
import {Review} from '../types/DomainTypes.ts';
import {useDependency} from '../services/Hooks.ts';
import {AdminService} from '../services/domain/AdminService.ts';
import {ActivityIndicator, Dimensions, FlatList, Text, View} from 'react-native';
import NavigateBackButton from '../components/buttons/NavigateBackButton.tsx';
import {ClientReviewsScreenProps} from '../types/NavigationTypes.ts';
import ReviewItem from '../components/listItems/ReviewItem.tsx';
import {Pressable} from 'react-native-gesture-handler';
import {SafeAreaView} from 'react-native-safe-area-context';

const d = Dimensions.get('screen');

export default function ClientReviewsScreen({navigation, route}: ClientReviewsScreenProps) {
  const adminService = useDependency<AdminService>('AdminService');

  const [reviews, setReviews] = useState<Review[] | null>(null);

  useEffect(() => {
    const getReviews = async () => {
      const response = await adminService.getReviews(route.params.clientId);

      if (response.content) {
        setReviews(response.content);
      }
    };
    getReviews();
  }, []);

  return (
    <SafeAreaView style={{flex: 1}}>
      <View style={{flex: 1, backgroundColor: 'white'}}>
        <View style={{alignItems: 'baseline', justifyContent: 'center', height: d.height * 0.086}}>
          <View style={{flexDirection: 'row', paddingHorizontal: 15}}>
            <NavigateBackButton
              navigation={navigation}
              onGoBack={undefined}/>
          </View>
          <Text
            style={{
              fontSize: 21,
              fontWeight: '600',
              color: 'black',
              alignSelf: 'center',
              position: 'absolute',
            }}>
            Отзывы
          </Text>
        </View>
        {reviews == null ? (
          <ActivityIndicator size="large" color={'white'} />
        ) : (
          <FlatList
            data={reviews}
            renderItem={item => (
              <Pressable
                onPress={() => navigation.navigate('EditReview', {review: item.item})}>
                <ReviewItem review={item.item}/>
              </Pressable>
            )}/>
        )}
      </View>
    </SafeAreaView>
  );
}
