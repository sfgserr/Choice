import {Dimensions, StyleSheet, View} from 'react-native';
import {ChatMessages} from '../types/DomainTypes.ts';
import React from 'react';
import LongRunningOperationIndicator from '../components/LongRunningOperationIndicator.tsx';
import {useDependency} from '../services/Hooks.ts';
import {ChatService} from '../services/domain/ChatService.tsx';

const d = Dimensions.get('screen');

export default function ChatScreen({id, navigation}: {id: string, navigation: any}) {
  const chatService = useDependency<ChatService>('ChatService');

  const [chat, setChat] = React.useState<ChatMessages | null>(null);

  React.useEffect(() => {
    const getChat = async () => {
      const response = await chatService.getChat(id);

      if (response.content != null) {
        setChat(response.content);
      }
    }
    getChat();
  }, []);

  return (
    <View style={styles.container}>
      {chat != null && (
        <View style={styles.chatBackground}>
          <View style={styles.userTab}>
            <View style={styles.horizontalSpread}>

            </View>
          </View>
        </View>
      )}
      <LongRunningOperationIndicator isRefreshing={chat == null}/>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'white'
  },
  chatBackground: {
    flex: 1,
    backgroundColor: '#F4F5FF'
  },
  userTab: {
    height: d.height * 0.108,
    width: d.width,
    position: 'absolute',
    top: 0,
    justifyContent: 'flex-end',
    backgroundColor: 'white'
  },
  horizontalSpread: {
    flexDirection: 'row',
    justifyContent: 'space-between'
  },
  bottomTab: {
    height: d.height * 0.108,
    width: d.width,
    position: 'absolute',
    bottom: 0,
    justifyContent: 'flex-start',
    backgroundColor: 'white',
    paddingHorizontal: 10
  },

});
