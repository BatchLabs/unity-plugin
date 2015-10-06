//
//  BatchPlugin.h
//  iosExtension
//
//  http://batch.com
//  Copyright (c) 2014 Batch SDK. All rights reserved.
//

#import <Foundation/Foundation.h>

#import <BatchBridge/BatchCallback.h>

#import "AppDelegateListener.h"

#define PluginVersion               @"1.4"

/*!
 @class BatchPlugin
 @abstract Unique Unity plugin object.
 */
@interface BatchPlugin : NSObject <AppDelegateListener>

/*!
 @method plugin
 @abstract Singleton accessor.
 @return The plugin object.
 */
+ (BatchPlugin *)plugin;

/*!
 @method call:withParameters:
 @abstract Perform an action from Unity to Batch.
 @param action      :   The available action string (see static ennumeration in BatchBridge.h).
 @param params      :   JSON compatible map of parameters.
 */
- (NSString *)call:(NSString *)action withParameters:(NSString *)params;

@end


/*!
 @class BatchPluginCallback
 @abstract Callback object.
 */
@interface BatchPluginCallback : NSObject <BatchCallback>

/*!
 @method call:withResult:
 @abstract Call actions from Batch to Unity.
 @param action  :   Result callback action string (see static ennumeration in BatchCallback.h).
 @param result  :   Action result can be (depending on the called action): NSString, NSNumber, NSArray or NSDictionary.
 */
- (void)call:(NSString *)action withResult:(id<NSSecureCoding>)result;

@end
