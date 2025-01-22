import React from 'react';
import {Dimensions, StyleSheet, Text, View} from 'react-native';
import {FillDataScreenProps} from '../types/NavigationTypes.ts';
import SocialMediasScreen from './SocialMediasScreen.tsx';
import AboutScreen from './AboutScreen.tsx';
import {GestureHandlerRootView} from 'react-native-gesture-handler';
import PickCategoriesBottomSheet from '../components/bottomSheets/PickCategoriesBottomSheet.tsx';
import BottomSheet from '@gorhom/bottom-sheet';
import {Category} from '../types/DomainTypes.ts';
import LongRunningOperationIndicator from '../components/LongRunningOperationIndicator.tsx';
import UnsuccessfulRequestModal from '../components/modals/UnsuccessfulRequestModal.tsx';
import SuccessfulRequestModal from '../components/modals/SuccessfulRequestModal.tsx';
import {State} from '../enums/AppEnums.ts';
import {useDependency} from '../services/Hooks.ts';
import {CompanyService} from '../services/domain/CompanyService.ts';
import {CategoryService} from '../services/domain/CategoryService.ts';
import {AuthContext} from '../contexts/authorized/Context.tsx';

const d = Dimensions.get('screen');

export default function FillDataScreen({route, navigation}: FillDataScreenProps) {
  const { changeState } = React.useContext(AuthContext);

  const companyService = useDependency<CompanyService>('CompanyService');
  const categoryService = useDependency<CategoryService>('CategoryService');

  const [socialMediaUris, setSocialMediaUris] = React.useState<string[]>([]);

  const [categories, setCategories] = React.useState<{category: Category; selected: boolean}[]>([]);

  React.useEffect(() => {
    async function getCategories() {
      let response = await categoryService.getCategories();

      if (response.content != null) {
        setCategories(response.content.map((i) => ({category: i, selected: false})));
      }
    }

    getCategories();
  }, []);

  const ref = React.useRef<BottomSheet>(null);

  const next = () => setCurrentIndex(prev => ++prev);

  const [isRefreshing, setIsRefreshing] = React.useState(false);

  const [isErrorToggled, setIsErrorToggled] = React.useState(false);
  const [errorMessage, setErrorMessage] = React.useState('');

  const [isToggled, setIsToggled] = React.useState(false);

  const fillData = async (description: string, photoUris: string[], prepaymentAvailable: boolean) => {
    setIsRefreshing(true);

    const response = await companyService.fillData(
      description,
      categories
        .filter(c => c.selected)
        .map(c => c.category.categoryId),
      photoUris,
      socialMediaUris,
      prepaymentAvailable);

    setIsRefreshing(false);

    if (response.result == 'successful') {
      setIsToggled(true);
    }
    else {
      setErrorMessage(response.error);
      setIsErrorToggled(true);
    }
  }

  const screens = [
    <SocialMediasScreen
      next={(socialMediaUris: string[]) => {
        setSocialMediaUris(socialMediaUris);
        next();
      }}/>,
    <AboutScreen
      next={async (description, photoUris, prepaymentAvailable) => {
        await fillData(description, photoUris, prepaymentAvailable);
      }}
      categoriesTitle={
        categories
          .filter(c => c.selected)
          .map(c => c.category.title)
          .join(',')
      }
      onChevronPressed={() => {
        ref.current?.expand();
      }}
    />,
  ];

  const [currentIndex, setCurrentIndex] = React.useState(0);

  const select = (index: number) => {
    setCategories(prev => {
      prev[index].selected = !prev[index].selected;

      return [...prev];
    })
  }

  return (
    <GestureHandlerRootView>
      <View style={styles.container}>
        <Text style={styles.title}>Карточка компании</Text>
        <View style={styles.screenContainer}>
          {screens.map((i, n) => (
            <View
              style={[styles.progressBar, {
                backgroundColor: currentIndex >= n ? '#2688EB' : '#DFDFDF',
                width: d.width/screens.length*0.85,
              }]}
              key={n}/>
          ))}
        </View>
        <View style={styles.splitter}/>
        {screens[currentIndex]}
      </View>
      <SuccessfulRequestModal
        isToggled={isToggled}
        handlePress={() => changeState(State.Unsubscribe)}
        title={'Отлично'}
        text={'Теперь тысячи пользователей увидят вашу компанию, вы сможете отвечать на их запросы'}/>
      <UnsuccessfulRequestModal
        isToggled={isErrorToggled}
        handlePress={() => setIsErrorToggled(prev => !prev)}
        errorMessage={errorMessage}/>
      <LongRunningOperationIndicator isRefreshing={isRefreshing}/>
      <PickCategoriesBottomSheet
        ref={ref}
        close={() => ref.current?.close()}
        categories={categories}
        select={select}/>
    </GestureHandlerRootView>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'white',
    paddingTop: 20,
    paddingHorizontal: 15
  },
  title: {
    fontSize: 21,
    fontWeight: '600',
    alignSelf: 'center',
    color: 'black'
  },
  screenContainer: {
    justifyContent: 'space-evenly',
    flexDirection: 'row',
    paddingTop: 20,
    paddingBottom: 20
  },
  progressBar: {
    height: 4,
    borderRadius: 5,
  },
  splitter: {
    backgroundColor: '#D7D8D9',
    width: 'auto',
    height: .25
  },
});
