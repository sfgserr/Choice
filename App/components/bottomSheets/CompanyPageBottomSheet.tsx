import React, {ForwardedRef} from 'react';
import {BottomSheetMethods} from '@gorhom/bottom-sheet/lib/typescript/types';
import CustomBottomSheet from './CustomBottomSheet.tsx';
import {CompanyInfo} from '../../types/DomainTypes.ts';
import {
  ActivityIndicator,
  Dimensions,
  FlatList,
  Image,
  View,
} from 'react-native';
import {CompanyService} from '../../services/domain/CompanyService.ts';
import {useDependency} from '../../services/Hooks.ts';
import {BottomSheetView} from '@gorhom/bottom-sheet';

type CompanyPageBottomSheetProps = {
  companyId: string
  close: () => void
};

const d = Dimensions.get('screen');

const CompanyPageBottomSheet = React.forwardRef(({companyId, close}: CompanyPageBottomSheetProps, ref: ForwardedRef<BottomSheetMethods>) => {
  const companyService = useDependency<CompanyService>('CompanyService');

  const [company, setCompany] = React.useState<CompanyInfo | null>(null);

  React.useEffect(() => {
    const getCompany = async () => {
      const response = await companyService.getCompanyOnMap(companyId);

      if (response.content != null)
        setCompany(response.content);
    }
    getCompany();
  }, []);

  return (
    <CustomBottomSheet
      ref={ref}
      title={'Компания'}
      close={close}>
      <BottomSheetView>
        {company == null ? (
          <View
            style={{
              flex: 1,
              justifyContent: 'center'
            }}>
            <ActivityIndicator
              size={'large'}
              color={'#2D81E0'}/>
          </View>
        ) : (
          <View
            style={{
              paddingHorizontal: 10
            }}>
            <View
              style={{
                width: 'auto',
                height: d.height*0.19,
                borderRadius: 15,
                backgroundColor: 'black',
              }}>
              <FlatList
                data={company.photoUris}
                renderItem={item => (
                  <Image
                    source={{uri: item.item}}
                    style={{
                      flex: 1,
                      borderRadius: 15
                    }}/>
                )}
                horizontal/>
            </View>
          </View>
        )}
      </BottomSheetView>
    </CustomBottomSheet>
  )
});

export default CompanyPageBottomSheet;
