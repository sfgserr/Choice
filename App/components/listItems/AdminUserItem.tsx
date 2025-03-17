import {AdminUser} from '../../types/DomainTypes.ts';
import {Image, StyleSheet, Text, TouchableOpacity, View} from 'react-native';
import React from 'react';

export default function AdminUserItem({item}: {item: AdminUser}) {
  return (
    <View style={styles.card}>
      <Image
        style={styles.icon}
        source={{uri: `${process.env.MINIO_URL}/app-files/${item.iconUri}`}}/>
      <View style={styles.dataContainer}>
        <Text style={styles.boldText}>{item.name}</Text>
        <Text style={styles.lightText}>{`${item.city}, ${item.street}`}</Text>
      </View>
      <TouchableOpacity style={styles.button} onPress={() => {}}>
        <Image
          source={require('../../assets/images/chevron-right.png')}
          style={styles.chevron}/>
      </TouchableOpacity>
    </View>
  );
}

const styles = StyleSheet.create({
  card: {
    paddingVertical: 10,
    flexDirection: 'row',
    paddingHorizontal: 15,
    borderBottomWidth: 1,
    borderColor: '#D7D8D9',
  },
  icon: {
    width: 40,
    height: 40,
    borderRadius: 360,
    resizeMode: 'cover',
    alignSelf: 'center',
  },
  dataContainer: {
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
    color: 'black',
  },
  lightText: {
    fontSize: 14,
    fontWeight: '400',
    color: '#99A2AD',
  },
  button: {
    position: 'absolute',
    alignSelf: 'center',
    right: 15,
  },
  chevron: {
    width: 15,
    height: 15,
    resizeMode: 'contain',
  },
});
