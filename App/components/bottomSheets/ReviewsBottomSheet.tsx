import CustomBottomSheet from './CustomBottomSheet.tsx';
import {BottomSheetFlatList, BottomSheetView} from '@gorhom/bottom-sheet';
import {CompanyInfo, Review} from '../../types/DomainTypes.ts';
import React, {ForwardedRef} from 'react';
import {useDependency} from '../../services/Hooks.ts';
import {ReviewService} from '../../services/domain/ReviewService.ts';
import ReviewItem from '../listItems/ReviewItem.tsx';
import {BottomSheetMethods} from '@gorhom/bottom-sheet/lib/typescript/types';
import {ActivityIndicator, View} from 'react-native';

const ReviewsBottomSheet = React.forwardRef(({close, company}: {close: () => void, company: CompanyInfo | null}, ref: ForwardedRef<BottomSheetMethods>) => {
  const reviewService = useDependency<ReviewService>('ReviewService');

  const [reviews, setReviews] = React.useState<Review[]>([]);

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
        {!reviews ? (
          <View style={{justifyContent: 'center', flex: 1}}>
            <ActivityIndicator size={'large'} color={'#2D81E0'} />
          </View>
          ) : (
          <BottomSheetFlatList
            data={reviews}
            renderItem={item => (
              <ReviewItem review={item.item}/>
            )}/>
        )}
      </BottomSheetView>
    </CustomBottomSheet>
  );
});

export default ReviewsBottomSheet;
