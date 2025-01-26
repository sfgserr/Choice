import React, {ForwardedRef} from 'react';
import {BottomSheetMethods} from '@gorhom/bottom-sheet/lib/typescript/types';
import CustomBottomSheet from './CustomBottomSheet.tsx';
import {CompanyInfo} from '../../types/DomainTypes.ts';
import {
  ActivityIndicator,
  Dimensions,
  Image,
  View,
} from 'react-native';
import {CompanyService} from '../../services/domain/CompanyService.ts';
import {useDependency} from '../../services/Hooks.ts';
import {BottomSheetView} from '@gorhom/bottom-sheet';
import {FlatList} from 'react-native-gesture-handler';

type CompanyPageBottomSheetProps = {
  companyId: string
  close: () => void
};

const d = Dimensions.get('screen');

const CompanyPageBottomSheet = React.forwardRef(({companyId, close}: CompanyPageBottomSheetProps, ref: ForwardedRef<BottomSheetMethods>) => {
  const companyService = useDependency<CompanyService>('CompanyService');

  const [company, setCompany] = React.useState<CompanyInfo | null>(null);

  const [currentIndex, setCurrentIndex] = React.useState(0);

  React.useEffect(() => {
    const getCompany = async () => {
      const response = await companyService.getCompanyOnMap(companyId);

      if (response.content != null)
        setCompany(response.content);
    }
    getCompany();
  }, [companyId]);

  return (
    <CustomBottomSheet ref={ref} title={'Компания'} close={close}>
      <BottomSheetView>
        {company == null ? (
          <View
            style={{
              flex: 1,
              justifyContent: 'center',
            }}>
            <ActivityIndicator size={'large'} color={'#2D81E0'} />
          </View>
        ) : (
          <View
            style={{
              paddingHorizontal: 10,
            }}>
            <View
              style={{
                backgroundColor: 'black',
                width: '100%',
                height: d.height * 0.19,
                borderRadius: 15,
                overflow: 'hidden'
              }}>
              <FlatList
                data={company.photoUris.filter(s => s != '')}
                onScroll={event => {
                  const totalWidth = event.nativeEvent.layoutMeasurement.width
                  const xPosition = event.nativeEvent.contentOffset.x
                  const newIndex = Math.round(xPosition / totalWidth)
                  if (newIndex !== currentIndex) {
                    setCurrentIndex(newIndex)
                  }
                }}
                renderItem={item => (
                  <View
                    style={{
                      width: d.width*0.86,
                      height: d.height * 0.19,
                      borderRadius: 15,
                      overflow: 'hidden',
                    }}>
                    <Image
                      source={{uri: `${process.env.MINIO_URL}/app-files/${item.item}`}}
                      style={{
                        width: '100%',
                        height: '100%',
                      }}
                    />
                  </View>
                )}
                horizontal
                pagingEnabled
                showsHorizontalScrollIndicator={false}
              />
              <View
                style={{
                  backgroundColor: 'black',
                  opacity: 0.5,
                  borderRadius: 8,
                  height: 15,
                  position: 'absolute',
                  alignSelf: 'center',
                  bottom: 5,
                  flexDirection: 'row',
                  justifyContent: 'center'
                }}>
                {company.photoUris.filter(s => s != '').map((s, i) => (
                  <View
                    style={{paddingHorizontal: 4, alignSelf: 'center'}}
                    key={i}>
                    <View
                      style={{
                        width: 8,
                        height: 8,
                        borderRadius: 4,
                        backgroundColor: 'white',
                        opacity: currentIndex == i ? 1 : 0.22
                      }}/>
                  </View>
                ))}
              </View>
            </View>
          </View>
        )}
      </BottomSheetView>
    </CustomBottomSheet>
  );
});

export default CompanyPageBottomSheet;
