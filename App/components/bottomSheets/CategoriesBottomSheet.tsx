import CustomBottomSheet from './CustomBottomSheet.tsx';
import CategoriesBottomSheetList from '../CategoriesBottomSheetList.tsx';
import {ForwardedRef} from 'react';
import {BottomSheetMethods} from '@gorhom/bottom-sheet/lib/typescript/types';
import * as React from 'react';

const CategoriesBottomSheet = React.forwardRef(({options, close}: any, ref: ForwardedRef<BottomSheetMethods>)=> {
  return (
    <CustomBottomSheet
      title={'Категория услуг'}
      ref={ref}
      close={() => close()}>
      <CategoriesBottomSheetList
        categories={options.categories}
        categoryIndex={options.categoryIndex}
        onIndexChange={options.onIndexChange}/>
    </CustomBottomSheet>
  );
});

export default CategoriesBottomSheet;
