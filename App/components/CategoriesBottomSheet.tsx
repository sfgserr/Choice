import CustomBottomSheet from './CustomBottomSheet.tsx';
import CategoriesBottomSheetList from './CategoriesBottomSheetList.tsx';
import {CategoriesBottomSheetProps} from '../types/ComponentTypes.ts';

export default function CategoriesBottomSheet({options, ref}: CategoriesBottomSheetProps) {
  return (
    <CustomBottomSheet
      title={'Категория услуг'}
      ref={ref}
      close={() => ref.current?.close()}>
      <CategoriesBottomSheetList
        categories={options.categories}
        categoryIndex={options.categoryIndex}
        onIndexChange={options.onIndexChange}/>
    </CustomBottomSheet>
  )
}
