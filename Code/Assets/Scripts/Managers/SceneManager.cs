using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EZCameraShake;
using System.Text;
public enum SCENE_STATE
{
	BEFORE_ACTION,
	AFTER_ACTION	
}
public class SceneManager : MonoBehaviour{
	public static SceneManager m_intance;
	public int m_status;
	public int m_cur_day = 0;
	public static readonly int TOTAL_DAY = 4;
    public int m_cur_scene;
	public List<int> m_scene_time = new List<int>();
	public Transform m_scene_root;
	public List<int> m_scene_history = new List<int>();
	public List<int> m_action_history = new List<int>();
	public static readonly string SCENE_PATH = "Assets/Resources/Scenes/";
	public int m_score = 5;//mood	
	public StringBuilder m_diary_builder = new StringBuilder();
	public bool m_end = false;
	//public static Dictionary<string, Transform> m_name_scene_map = new Dictionary<string, Transform>();
	

	void Awake()
	{
		Singleton<SceneManager>.Instance = this;
		m_intance = this;
		Messenger.AddListener<int>("scene_set", SetScene);
		Messenger.AddListener("scene_hide", HideScene);
		Messenger.AddListener("dialog_end", EndDialog);
		Messenger.AddListener("dialog_choose", ChooseAction);

		Messenger.AddListener<int>("calender_close", CloseCalender);
		Messenger.AddListener<int>("choose_chosen", ActionChosen);

		for (int i = 0; i < (int)SCENE_TYPE.COUNT; ++i)
		{
			m_scene_time.Add(0);
		}
	}
	public void Init(Transform tran)
    {
		m_cur_scene = (int)SCENE_TYPE.POLE;
		m_scene_root = tran;
		m_status = (int)SCENE_STATE.BEFORE_ACTION;
    }

	//public Transform GetScene(string name)
	//{
	//	if (!m_name_scene_map.ContainsKey(name))
	//	{
	//		GameObject scene_prefab = (GameObject)Resources.Load(SCENE_PATH + name);
	//		Transform scene_tran = Instantiate(scene_prefab).transform;
	//		m_name_scene_map.Add(name, scene_tran);
	//	}
	//	return m_name_scene_map[name];
	//}

	public void SetScene(int scene)
	{
		m_cur_scene = scene;
		m_status = (int)SCENE_STATE.BEFORE_ACTION;

		for (int i = 0; i < transform.childCount; ++i)
		{
			if (i == scene)
			{
				transform.GetChild(i).gameObject.SetActive(true);
			}
			else
			{
				transform.GetChild(i).gameObject.SetActive(false);
			}
		}
		if(scene > (int)SCENE_TYPE.COUNT)
		{
			return;
		}
		

		List<DialogNode> content_list = new List<DialogNode>();
		//int scene_time = GetSceneTime(scene);
		switch (scene)
		{
			case (int)SCENE_TYPE.MONTAIN:
				if (m_scene_history.Contains((int)SCENE_TYPE.MONTAIN))
				{
					Singleton<AudioManager>.Instance.PlayMusic(MUSIC_TYPE.village);
					m_status = (int)SCENE_STATE.AFTER_ACTION;
					content_list.Add(new DialogNode("又回到了这座大山，我想再去拜访一次那株神木。"));
					content_list.Add(new DialogNode("毕竟，这样的奇观不是谁都能看到的，而且我也时日不多，再说这个秘密也只有我一个人知道。"));
					content_list.Add(new DialogNode("我按着上次的记忆，走进了树林，竟然还真的让我找回了那株巨木。"));
					content_list.Add(new DialogNode("我对它寄予了深厚的感情，好像它是我千古难寻的知音一样。"));
					content_list.Add(new DialogNode("我情不自禁地走上前去，轻轻抚摩着它那粗糙而又有厚重年代感的树皮。"));
					content_list.Add(new DialogNode("想到这棵树，我就会很自然地想到上次的那个村落。"));
					content_list.Add(new DialogNode("他们现在怎么样了呢？我的好奇心萌发，带着我沿着上次的道路走去。"));
					content_list.Add(new DialogNode("看来我的记忆力还是挺不错的，我顺利地找到了那个村庄。"));
					content_list.Add(new DialogNode("为了避免再被他们抓起来，我只是偷偷地躲在大树背后看着。"));
					content_list.Add(new DialogNode("刚好有几个居民在不远处交谈，我看着他们，觉得还是不要打扰这个世外桃源。"));
					content_list.Add(new DialogNode("正想走时，却踩到了一条干树枝。“啪嚓”。这次完蛋了。"));
					content_list.Add(new DialogNode("他们又把我扔到之前那个豪华的座位上，用他们的土语交谈着。"));
					content_list.Add(new DialogNode("我想起之前看过的一部电影，觉得他们估计是要把我煮了来释放我肉体里神的灵魂。"));
					content_list.Add(new DialogNode("我的心里忐忑无比，一个看起来应该是长老的人拿着一个树枝做的头环和一根粗制的权杖走过来，单膝跪下，把这些东西呈给我。"));
					content_list.Add(new DialogNode("我头脑风暴了一秒钟，这不会是要我当村长吧？"));
					content_list.Add(new DialogNode("我跟长老比划着，大概的意思是我可以当村长，但是我需要时常外出，如果你们不同意的话，那我就不做了。"));
					content_list.Add(new DialogNode("长老花了挺长一段时间来理解我是什么意思，又花了挺长一段时间来和村民交谈"));
					content_list.Add(new DialogNode("最终他走过来，向我点点头。"));
					content_list.Add(new DialogNode("我长呼一口气，还以为我为数不多的时日就要在这里坐着浪费掉了。"));
					content_list.Add(new DialogNode("我接过头环和权杖，想到能够当这样一个村子的村长，还有点小激动。"));
					m_score += 3;					
					m_diary_builder.Append(GetScoreDescription(3));
				}
				else
				{
					Singleton<AudioManager>.Instance.PlayMusic(MUSIC_TYPE.mountain);
					content_list.Add(new DialogNode("来到高山之后，我感觉我的心灵都被洗涤了。"));
					content_list.Add(new DialogNode("山风吹拂着我的脸，似乎能带走一切的烦恼，耳边传来的都是清脆的鸟鸣和树木摆动的声音，在这么宁静的地方，我的灵魂都似乎得到了净化一般。"));					
				}
				Singleton<DialogManager>.Instance.ShowDialog(content_list);
				break;
			case (int)SCENE_TYPE.START:
				m_status = (int)SCENE_STATE.AFTER_ACTION;
				Singleton<AudioManager>.Instance.PlayMusic(MUSIC_TYPE.start);
				switch (m_cur_day)
				{
					case 0:
						content_list.Add(new DialogNode("医生:", "对不起，你的生命只剩下七天的时间了。"));
						content_list.Add(new DialogNode("为……为什么，一定还有方法的对不对。"));
						content_list.Add(new DialogNode("医生:", "我们已经尽力了，请好好的享受剩余的人生吧。"));
						content_list.Add(new DialogNode("怎么会……可恶，我不想死啊。"));
						content_list.Add(new DialogNode("既然如此，我要去我想去的地方，做所有想做的事情，不让我的生命留下遗憾。"));
						break;
					case 1:
						content_list.Add(new DialogNode("第一天过去了"));
						break;
					case 2:
						content_list.Add(new DialogNode("第二天过去了"));
						break;
					case 3:
						content_list.Add(new DialogNode("第三天过去了"));
						break;
					case 4:
						if(m_score <= 0)
						{
							content_list.Add(new DialogNode("死亡与每个人都是不必着急的事。"));
							content_list.Add(new DialogNode("或早或晚，或急或缓，总会到来。"));
							content_list.Add(new DialogNode("然而我却是不愿再等——"));
							content_list.Add(new DialogNode("虽然我需要等待的时间也不会多——我迫不及待地要离开这个丑恶的世界。"));
							content_list.Add(new DialogNode("吞服下毒药，我闭上眼静静地等待死亡的到来。"));
							content_list.Add(new DialogNode("我希望不要有轮回，因为我不想再和这个世界有任何纠葛。"));

						}
						else if(m_score <= 10)
						{
							content_list.Add(new DialogNode("这一刻终于来了。"));
							content_list.Add(new DialogNode("我能感觉到死神在我的身边，我的力量一点一点地被剥夺，我的意识在被黑暗不断地吞噬。"));
							content_list.Add(new DialogNode("但我的内心却是平静甚至是期待的。"));
							content_list.Add(new DialogNode("世界已没有任何值得我留恋的东西了。"));
							content_list.Add(new DialogNode("死亡于我，是一种解脱，我渴望已久的解脱。"));

						}
						else if(m_score <= 20)
						{
							content_list.Add(new DialogNode("尽管没有任何的预兆，"));
							content_list.Add(new DialogNode("但我还是感觉到，"));
							content_list.Add(new DialogNode("我的终焉之刻来临了。"));
							content_list.Add(new DialogNode("努力维持着意识，但却已力不从心。"));
							content_list.Add(new DialogNode("腿脚一阵无力，不甘心地跪倒在地。"));
							content_list.Add(new DialogNode("可恶，我还想有更多的时间，"));
							content_list.Add(new DialogNode("再一分钟。"));
							content_list.Add(new DialogNode("再一小时。"));
							content_list.Add(new DialogNode("再一个七天。"));
						}
						else
						{
							content_list.Add(new DialogNode("一股困倦感袭来，"));
							content_list.Add(new DialogNode("我的力量正从四肢百骸中失去，"));
							content_list.Add(new DialogNode("呼吸也变得困难起来。"));
							content_list.Add(new DialogNode("这就是死亡的感觉吗？"));
							content_list.Add(new DialogNode("努力睁开双眼，"));
							content_list.Add(new DialogNode("虽然视线已变得模糊"));
							content_list.Add(new DialogNode("但我还能看见这世界"));
							content_list.Add(new DialogNode("我做了所有我想做的，生命中已了无遗憾。"));
							content_list.Add(new DialogNode("我缓缓闭上已无力支撑的眼皮，我尽力保持微笑。"));
							content_list.Add(new DialogNode("因为我要为这个世界，留下最后一点美。"));
						}
						break;
				}
				Singleton<DialogManager>.Instance.ShowDialog(content_list);
				break;
			case (int)SCENE_TYPE.POLE:
				if (m_scene_history.Contains((int)SCENE_TYPE.MONTAIN))
				{
					Singleton<AudioManager>.Instance.PlayMusic(MUSIC_TYPE.pole);

					m_status = (int)SCENE_STATE.AFTER_ACTION;
					content_list.Add(new DialogNode("看到远处的小黑点，我突然灵光一闪，这似曾相识的感觉，应该是在什么地方曾经见过。"));
					content_list.Add(new DialogNode("于是我打起精神走过去，走进了之后才发现原来是一片废墟，却又不仅仅是一片废墟。"));
					content_list.Add(new DialogNode("在这片废墟之中，我找不到任何熟悉的文明的痕迹，反而是处处散发着一股浓郁的不协调感，这种感觉难道就是所谓的外星文明吗？"));
					content_list.Add(new DialogNode("就在我还在打量的时候，面对着我的金属大门突然打开，看了看黑黝黝的大门，我犹豫了一会还是选择了进去。"));
					content_list.Add(new DialogNode("我进入里面之后发现是一个类似祭坛的空旷场所，正中有着一具尸体——其实我也不确定是不是尸体"));
					content_list.Add(new DialogNode("不过就这么叫吧，周围的位置似乎是摆放祭品什么的。"));
					content_list.Add(new DialogNode("就在我转身准备离开的时候，却突然看见前方的黑暗中有一对闪烁的大眼睛。"));
					content_list.Add(new DialogNode("吓得我倒退了几步。“什么人。”或者我应该问，什么东西。"));
					content_list.Add(new DialogNode("我看到那个大眼睛的时候就感觉情况不妙，对着大门狂奔而去"));
					content_list.Add(new DialogNode("这时大门却突然开始关上，而我身后的大眼睛的距离越来越近了——"));
					content_list.Add(new DialogNode("因为有些冰凉的东西已经摸到了我的脖子。"));
					content_list.Add(new DialogNode("“咚” 大门沉重地关上，而我在门前又踢又打，这金属大门却是不为所动"));
					content_list.Add(new DialogNode("而我身后的东西，已经来到了我的面前。"));
					content_list.Add(new DialogNode("“你是，祭品。”"));
					m_end = true;
				}
				else if (m_scene_history.Contains((int)SCENE_TYPE.LAB))
				{
					Singleton<AudioManager>.Instance.PlayMusic(MUSIC_TYPE.monster);
					m_status = (int)SCENE_STATE.AFTER_ACTION;
					content_list.Add(new DialogNode("极地被一片白雪覆盖的景象还是很壮观的。"));
					content_list.Add(new DialogNode("我艰难地迈着步子，远处突然有一个庞大的黑影飞快地闪过"));
					content_list.Add(new DialogNode("我揉揉眼，却没看到什么东西，大概是眼花吧，我也没怎么放在心上。"));
					content_list.Add(new DialogNode("那个黑影又出现了，虽然还是飞快地闪过，但这次好像更大了。"));
					content_list.Add(new DialogNode("我有点慌乱，但想到在这样一个地方，也没有能躲藏的地点，我干脆继续往前行走。"));
					content_list.Add(new DialogNode("它又出现了，原来不是黑影，而是一个……人"));
					content_list.Add(new DialogNode("形的生物。它的体型和速度绝对不是普通人类能达到的。"));
					content_list.Add(new DialogNode("为什么它在放大呢？"));
					content_list.Add(new DialogNode("……只有一个可能"));
					content_list.Add(new DialogNode("它在朝我冲过来。"));
					content_list.Add(new DialogNode("想到这里，我一下慌了手脚，不知怎么办才好，它已经接近得我能够看清楚了，原来是传说中的一种叫雪人的古生物。"));
					content_list.Add(new DialogNode("它的速度不快，但步子大得可怕。"));
					content_list.Add(new DialogNode("我还没来得及反应过来，就已经被它一爪子拍倒在地。"));
					content_list.Add(new DialogNode("它的力量也和它的体型成正比，我的颧骨好像已经断掉了。"));
					content_list.Add(new DialogNode("它不断地用爪子和脚攻击着我，我忍住疼痛，拼命地想着应对方法。"));
					content_list.Add(new DialogNode("它的体型和进攻方法跟熊有点像，我干脆就用对付熊的方法对付它——装死。我尽力让自己的四肢放松，屏住呼吸。"));
					content_list.Add(new DialogNode("它还真的停止了进攻，我拖着勉强还能移动的手臂，掏出发信器，给最近的考察站发了求救信号。"));
					content_list.Add(new DialogNode("醒来的时候，我已经在一个不知名城市里的医院了。"));
					content_list.Add(new DialogNode("我的身旁坐着一个我不认识的人，他自我介绍说是我国家在这里的大使，负责我的后续和回国的工作。"));
					content_list.Add(new DialogNode("我谢过了他，然后接下来几天都跟着他，总算是回到了家。"));
					content_list.Add(new DialogNode("果然去荒无人烟的地方探险，是风险很大的啊。"));
					m_score -= 3;
					m_diary_builder.Append("太惊险了，遭遇了奇怪的怪兽的袭击，白色毛发的猛兽在极地里追逐我，幸好我跑得快，不然的话已经沦为它的盘中餐了。这种怪兽肯定是人类污染环境才导致它们出现的，无可救药的人类。");
					m_diary_builder.Append(GetScoreDescription(-3));
				}
				else
				{
					Singleton<AudioManager>.Instance.PlayMusic(MUSIC_TYPE.pole);

					content_list.Add(new DialogNode("经过了一番艰难的跋涉之后，我终于站在了这片被冰雪统治的地域。"));
					content_list.Add(new DialogNode("放眼所及都是白，炫目的白，仿佛天地间都只余下这一种颜色，地上偶尔一抹顽强的绿色和远处的极地生物们，都是这里的精灵。"));
					content_list.Add(new DialogNode("而我作为一个外来者，只能虔诚地朝拜。其实人如果也能这么一直保持纯白的话，是多么美好的一件事。"));
				}

				Singleton<DialogManager>.Instance.ShowDialog(content_list);
				break;
			case (int)SCENE_TYPE.DACE_HALL:
				if (m_scene_history.Contains((int)SCENE_TYPE.HOSPITAL))
				{
					Singleton<AudioManager>.Instance.PlayMusic(MUSIC_TYPE.sex_change);

					m_status = (int)SCENE_STATE.AFTER_ACTION;
					content_list.Add(new DialogNode("在歌舞厅里，我刚坐下没多久就发现有个男生对着我的座位频频投来目光，不一会儿居然拿着酒杯走了过来。"));
					content_list.Add(new DialogNode("看到的第一眼的时候我就感觉他似乎有点面善，但是却想不起来到底是在哪里见过面。"));
					content_list.Add(new DialogNode("搭讪了几句之后他给我倒了一杯酒，我没有想太多就喝了下去，一阵天旋地转的眩晕感袭来。"));
					content_list.Add(new DialogNode("当我醒来的时候已经是在酒店里面，而自己正全身赤裸地躺在酒店里，身边躺着的正是他。"));
					content_list.Add(new DialogNode("我惊慌地问为什么，他居然说出了一个让我目瞪口呆的事实。"));
					content_list.Add(new DialogNode("原来他原本是她，也就是一个女生，一次和我告白之后被我拒绝了，我随口说了一句“其实我喜欢的是男人。”"));
					content_list.Add(new DialogNode("没想到她居然就牢记于心，暗自下定决心要成为我喜欢的人。"));
					content_list.Add(new DialogNode("终于在前几天去到医院了做了变性手术，还遇到了我——那时我们在医院里撞了一下。"));
					content_list.Add(new DialogNode("而后下了一点迷药终于得到了我"));
					content_list.Add(new DialogNode("看到他，一个男人对着我一脸迷恋的样子，我浑身恶寒"));
					content_list.Add(new DialogNode("抓起衣服夺门而出，感觉这个世界都充满了恶意。"));
					m_score -= 3;
					m_diary_builder.Append("最糟糕的经历！我被一个由女人变成的男人睡了。没想到当年随口一说的话居然一语成谶，所以以后我要谨言慎行，以免再次重蹈覆辙。至于这次的时间，我还是快点忘记吧，这是一场噩梦。");
					m_diary_builder.Append(GetScoreDescription(-3));
				}
				else
				{
					Singleton<AudioManager>.Instance.PlayMusic(MUSIC_TYPE.sex_change);

					content_list.Add(new DialogNode("来到这个醉生梦死的地方，作为本地最大的娱乐场所，鱼龙混杂，或许也会有几个和我一样的生命踏入了倒计时的人吧。"));
					content_list.Add(new DialogNode("我们这样的人来到这里的目的就是为了买醉，唯有酒精侵占大脑的时候，才能让我们暂时忘却所有的烦恼。"));
				}
				Singleton<DialogManager>.Instance.ShowDialog(content_list);
				break;
			case (int)SCENE_TYPE.LAB:
				if (m_scene_history.Contains((int)SCENE_TYPE.MONTAIN))
				{
					Singleton<AudioManager>.Instance.PlayMusic(MUSIC_TYPE.snow_pet);

					m_status = (int)SCENE_STATE.AFTER_ACTION;
					content_list.Add(new DialogNode("说起来，上次在极地调查时得到了一个奇怪的骨骸，一直想着要拿去给研究所的人看看，就今天吧。"));
					content_list.Add(new DialogNode("一开始的时候他们还不相信，以为我拿了一个石头欺骗他们，然而在经过了年代测定和基因分析之后，他们的脸色都变了。"));
					content_list.Add(new DialogNode("一个看起来像是学者的人颤抖着拉起我的手，仔细地询问我到底是如何得到这个东西的"));
					content_list.Add(new DialogNode("我只能告诉他是我偶然的情况下得来的。"));
					content_list.Add(new DialogNode("这时我一直放在口袋里的手拿手机时不小心带出了几根白色的纤维，我不以为然地想要甩开"));
					content_list.Add(new DialogNode("那个学者如获珍宝地把这些纤维收集起来，一番捣鼓之后兴奋地邀请我再来研究所"));
					content_list.Add(new DialogNode("因为他们即将公布一个重大的发现，因为我而得到了突破性进展。"));
					content_list.Add(new DialogNode("之后我再次去到他们的研究所，却发现他们在准备开记者招待会，因为据说他们复活了传说中的雪人"));
					content_list.Add(new DialogNode("——据他们解释就是一种凶猛的食肉灵长类，和猩猩可能是近亲。"));
					content_list.Add(new DialogNode("于是我被安排到会客室里等候，然而我坐下没多久的时候就听见研究所里传来一阵骚动"));
					content_list.Add(new DialogNode("连忙打开门去看，却看见一只全身雪白，两米多高的怪兽在研究所里横冲直撞"));
					content_list.Add(new DialogNode("一对粗壮的手臂充满了力量，亲眼看见铁栏杆被它扯下来拧成了麻花"));
					content_list.Add(new DialogNode("我吓的立刻关上门口，打算从窗户逃生，然而还不到两秒钟大门就被它撞开"));
					content_list.Add(new DialogNode("似乎是专盯着我一般的直冲我而来，一把将我拦腰抓住，一手抓住我的脑袋，轻轻地一拧。"));
					content_list.Add(new DialogNode("“咔嚓。”"));
					m_end = true;
				}
				else if(m_scene_history.Contains((int)SCENE_TYPE.HOSPITAL))
				{
					Singleton<AudioManager>.Instance.PlayMusic(MUSIC_TYPE.humman);

					m_status = (int)SCENE_STATE.AFTER_ACTION;
					content_list.Add(new DialogNode("想起上次在医院看见的那些研究员，还有他们的尸体交易。"));
					content_list.Add(new DialogNode("我内心十分想探寻出这背后隐藏的阴谋，于是就偷偷闯进了非开放研究区。"));
					content_list.Add(new DialogNode("但没等我找到上次那一批人，我就被警卫抓住了。"));
					content_list.Add(new DialogNode("有一群科学家正好经过，就拦下了想把我送去警察局的警卫。"));
					content_list.Add(new DialogNode("他们对我说：“小伙子，擅闯国家级的实验室，这可是大罪啊。"));
					content_list.Add(new DialogNode("你不想进监狱，不想被判死刑吧"));
					content_list.Add(new DialogNode("这样，我们这里有一个实验，需要一个志愿者，我要坦白跟你说，有生命危险"));
					content_list.Add(new DialogNode("但是比起死刑，又还有一线希望。”"));
					content_list.Add(new DialogNode("“反正我也时日不多了，那干脆就赌一把。”"));
					content_list.Add(new DialogNode("“好！放开他吧。”"));
					content_list.Add(new DialogNode("这个戴着防毒面具，声音和善的大叔科学家把我带到一个实验室，交给了另一群科学家。"));
					content_list.Add(new DialogNode("他们拿给我一个协议，要我在上面签名。"));
					content_list.Add(new DialogNode("我写上了自己的名字，他们把我带进里面的一个房间，让我躺在冰冷的手术床上，用铁链绑住我的手脚。"));
					content_list.Add(new DialogNode("我有点本能的恐惧，但还是尽力压制。"));
					content_list.Add(new DialogNode("一个科学家拿过来一根针筒，注射进了我的身体。"));
					content_list.Add(new DialogNode("我只感觉一阵炽热，五脏六腑好像都扭成一团，是一种难以言喻的痛苦，幸好，没有一分钟，我就晕了过去。"));
					content_list.Add(new DialogNode("等我醒来时，还是在手术床上，还是一群白大褂围着我，有不少人手上都拿着笔和记录板。"));
					content_list.Add(new DialogNode("其中一个开口问我：“你还会说话吗？”"));
					content_list.Add(new DialogNode("“会。”"));
					content_list.Add(new DialogNode("他们爆发出一阵欢呼，互相击掌。"));
					content_list.Add(new DialogNode("然后又问了我几个智障级别的问题，我当然都如实回答了。"));
					content_list.Add(new DialogNode("他们给我解开了铁链，跟我解释发生了什么："));
					content_list.Add(new DialogNode("“我们做的是变种人的实验，尸体是用来做临床反应测试的，但是死人和活人毕竟有区别，我们一直苦于没有志愿者。"));
					content_list.Add(new DialogNode("但是现在，恭喜你，你成为了世界上第一个变种人。”"));
					content_list.Add(new DialogNode("他向我伸出手，我还有点发愣，本能地握住了他的手。"));
					content_list.Add(new DialogNode("他看到我的反应，不禁又害怕起来。"));
					content_list.Add(new DialogNode("我笑笑，跟他解释只是我还没有理清楚发生了什么而已，他才又放心下来。"));
					content_list.Add(new DialogNode("我突然想起还有一件很重要的事情。"));
					content_list.Add(new DialogNode("这样，能治好我的病吗？"));
					content_list.Add(new DialogNode("十年后。"));
					content_list.Add(new DialogNode("“世界上最后一个普通人类，今天也接受了注射，成为了一名变种人。"));
					content_list.Add(new DialogNode("至此，地球上所有人类都成为了变种人，我们也不应该再称呼自己为变种人，而是——"));
					content_list.Add(new DialogNode("新人类。”"));
					content_list.Add(new DialogNode("作为世界上第一个变种人，我曾经也出名过一段时间，但随着这项技术越来越普及，我就又逐渐回到了正常人的身份。"));
					content_list.Add(new DialogNode("幸好，我在出名那段时间捞到了一份不错的工作，现在的日子，也能说是比较富裕吧。"));
					content_list.Add(new DialogNode("我的担忧，也在注射后一个星期完美地解决了，我还记得当时那种心情，让我透彻地理解了“欣喜若狂”这个成语。"));
					content_list.Add(new DialogNode("我也和当初改变我人生的那位大叔研究员成了好朋友，他现在已经是一个白发苍苍的老人了。我也会经常找他讨论人生。"));
					content_list.Add(new DialogNode("我和他还有一个同样的忧虑：人类这样改变自然规律，会不会有一天打破自然平衡，使我们的灭顶之灾提前到来？"));
					m_diary_builder.Append("\n\n\n没想到我还找得到这本日记本。新人类，这个名字倒是挺好听的，只是希望这个造福人类的研究，不要好心办坏事，加速人类的灭亡。");
					m_end = true;
				}
				else
				{
					Singleton<AudioManager>.Instance.PlayMusic(MUSIC_TYPE.lab);

					content_list.Add(new DialogNode("这是一个著名的研究所，里面进行的研究都是处于前端的高精尖技术，里面的人们日以继夜地奋斗着。"));
					content_list.Add(new DialogNode("只是不知道他们能不能找到让我把生命延续下去的方法呢？"));
					content_list.Add(new DialogNode("不过就算能，也轮不到我这样的平民来享受吧。"));
					
				}
				Singleton<DialogManager>.Instance.ShowDialog(content_list);
				break;
			case (int)SCENE_TYPE.HOME:
				if (m_scene_history.Contains((int)SCENE_TYPE.DACE_HALL))
				{
					Singleton<AudioManager>.Instance.PlayMusic(MUSIC_TYPE.brother);

					m_status = (int)SCENE_STATE.AFTER_ACTION;
					content_list.Add(new DialogNode("走到家门口，我突然想看看邻居这几年来有什么变化，就跨进了邻居家院子的大门。"));
					content_list.Add(new DialogNode("院子里停着一辆小车，房屋也重新装修过，看来邻居还是混得挺风生水起的。"));
					content_list.Add(new DialogNode("我听见屋子里有人在交谈的声音，想到不如进去寒暄几句，拜访一下多年未见的邻居大爷大妈。"));
					content_list.Add(new DialogNode("刚走进屋子，就看到那个在歌舞厅里曾经遇见的保安。"));
					content_list.Add(new DialogNode("我和他都愣住了，一种强烈的熟悉感在我心中涌起。"));
					content_list.Add(new DialogNode("我马上把父母找到邻居家里，问他们是怎么回事。"));
					content_list.Add(new DialogNode("他们刚开始也有点惊讶，然后和邻居的大爷大妈激烈地讨论了半小时，母亲才向我缓缓道来："));
					content_list.Add(new DialogNode("“其实生你的时候，你还有一个双胞胎哥哥"));
					content_list.Add(new DialogNode("但正巧那天咱邻居家里也有喜，我们两家人又在同一个医院"));
					content_list.Add(new DialogNode("应该是护士没留意看或者太忙，我们两家人的信息又十分相近，就阴差阳错地把你哥哥抱到了他们家。"));
					content_list.Add(new DialogNode("他们家的孩子又不知道哪里去了。"));
					content_list.Add(new DialogNode("当时我还伤心的很呢，明明记得生了俩，咋一出来就只剩一个了？"));
					content_list.Add(new DialogNode("我和你爹去问护士，才知道是这么回事，但那时候那个护士还不记得把孩子抱哪去了，也追不回来。"));
					content_list.Add(new DialogNode("这么多年了，终于真相大白了。”"));
					content_list.Add(new DialogNode("我听了之后恍然大悟，原来我们两个人的熟悉感是因为血缘有关系。"));
					content_list.Add(new DialogNode("我提议和他去做个血缘鉴定，他也不假思索地同意了。"));
					content_list.Add(new DialogNode("一段时间之后，鉴定结果出来了，结果和母亲讲的一样，我们真的是亲生的双胞胎兄弟。"));
					content_list.Add(new DialogNode("回到家里，我和父母商量给邻居的大爷大妈一点钱"));
					content_list.Add(new DialogNode("毕竟他们养了这么多年的孩子，结果发现是我们家的"));
					content_list.Add(new DialogNode("一是还他们这么多年的抚养费，二是给他们以后的生活一点资助。"));
					content_list.Add(new DialogNode("父母没等我说完，就连连点头。"));
					content_list.Add(new DialogNode("父亲拿着一个厚厚的信封，带着我走进他们家里，把信封悄悄地放在桌子上。"));
					content_list.Add(new DialogNode("虽然我们家的资金一下少了三十万，但我的心情却十分舒畅。"));
					m_diary_builder.Append("没有想到我居然还有一个双胞胎兄弟，而且还让我们在茫茫人海之中相遇了，真是造化弄人。我们家里给他们一笔钱作为补偿，虽然不能说明什么，但是至少是我们的一点心意吧。");	
				}
				else if (m_scene_history.Contains((int)SCENE_TYPE.LAB))
				{
					Singleton<AudioManager>.Instance.PlayMusic(MUSIC_TYPE.zombie);

					m_status = (int)SCENE_STATE.AFTER_ACTION;
					content_list.Add(new DialogNode("那天从研究所回来之后我感觉身体总是不太对劲的感觉，而今天一看居然发烧了，真是悲伤，没想到在最后的日子里我居然还要感冒。"));
					content_list.Add(new DialogNode("不想去医院浪费钱的我决定回家乡住一段时间，回到家里父母都热情地欢迎我回来，毕竟我已经有许久没有回家了。"));
					content_list.Add(new DialogNode("而家乡里的一切都让我感到如此的熟悉亲切。"));
					content_list.Add(new DialogNode("迎着清爽的山风，一时没有适应这个温度的变化的我连打了好几个喷嚏，擦了擦鼻子，赶紧回到家里睡觉。"));
					content_list.Add(new DialogNode("今天似乎睡得特别的沉，醒来的时候感觉身体的发热症状似乎又严重了一点。"));
					content_list.Add(new DialogNode("走出房间门，却发现屋子里面一片狼藉，更可怕的是屋子里居然有两个……"));
					content_list.Add(new DialogNode("我一下愣住了，不知道该用什么形容词，我在脑海里搜索着和他们相近的信息，突然想到："));
					content_list.Add(new DialogNode("这不是僵尸吗！"));
					content_list.Add(new DialogNode("原本或坐或卧在地上的僵尸，一察觉到我的到来之后似乎突然觉醒了一般，从地上一跃而起攻击我。"));
					content_list.Add(new DialogNode("猛地推开那两具僵尸，我夺门而逃。"));
					content_list.Add(new DialogNode("然而昨天还是宁静山村的家乡，今天却到处都是僵尸在游荡"));
					content_list.Add(new DialogNode("当我跑出大街时看到的正是无数个僵尸齐齐地看过来，他们空洞的眼神里余留下的，只有对于新鲜血肉的渴望而已。"));
					content_list.Add(new DialogNode("九死一生地逃出僵尸的围攻，好不容易地拦截下一辆公车，幸好车上的人都还一无所知"));
					content_list.Add(new DialogNode("坐在座位上的我还惊魂未定，然而我身体的不适感却越来越强。"));
					content_list.Add(new DialogNode("一滴口水滴落在地上，坐在我对面的妇女惊呼着从座位上站起来，我抬起头看见镜子里的自己赫然正是刚才的僵尸的模样"));
					content_list.Add(new DialogNode("然而我现在已经没有更多的时间去思考这些了，或者说我不愿意去思考，因为我现在满脑子的都只有一个想法。"));
					content_list.Add(new DialogNode("我要吃肉。"));
					m_diary_builder.Append("可恶，到底发生了什么？为什么会突然出现这么多丧尸，我要赶紧离开那里。意识（凌乱的笔迹）模糊了，想吃（凌乱的笔迹）肉（血滴和凌乱的笔迹）");
					m_end = true;
				}
				else
				{
					Singleton<AudioManager>.Instance.PlayMusic(MUSIC_TYPE.hometown);

					content_list.Add(new DialogNode("我出生的地方，是一片美丽的山村。"));
					content_list.Add(new DialogNode("现在想来，我已经有许多年没有回来了。"));
					content_list.Add(new DialogNode("家乡早已模糊成为了一个符号，每年都要思考回还是不回的困难抉择。"));
					content_list.Add(new DialogNode("记忆中的许多事物，如今都定格成了回忆。"));
					content_list.Add(new DialogNode("我觉得在这个时候，我应该回去看看我的父母，再看看这个养育我的美丽的地方，顺便也为自己，找一个长眠之所。"));
				}

				Singleton<DialogManager>.Instance.ShowDialog(content_list);
				break;
			case (int)SCENE_TYPE.HOSPITAL:
				if (m_scene_history.Contains((int)SCENE_TYPE.MONTAIN))
				{
					Singleton<AudioManager>.Instance.PlayMusic(MUSIC_TYPE.cured);

					m_status = (int)SCENE_STATE.AFTER_ACTION;
					content_list.Add(new DialogNode("在医院里，为我检查身体的医生惊奇地发现，我身体的情况似乎有所好转"));
					content_list.Add(new DialogNode("惊讶地问我到底去过了什么地方，我努力地回想，也只能告诉他我去过那座山而已。"));
					content_list.Add(new DialogNode("“原来如此，那座山上有个神秘的巫医部落，据说在治疗有些疾病方面有着现代医学都无法比拟的手段。你何不再去探索一下看有什么结果呢。”"));
					content_list.Add(new DialogNode("我立刻启程前往那座山，果然在那里找到了那个神秘的部落....."));
					content_list.Add(new DialogNode("之后回到了医院，我利用巫医和现代医学的结合，成功地治愈了我的疾病"));
					content_list.Add(new DialogNode("我的生命再次被延长了。"));
					content_list.Add(new DialogNode("然而当医生对我宣布这个消息的时候，我却没有想象中的欢呼雀跃，反而在心中有种淡淡的惆怅。"));
					content_list.Add(new DialogNode("这是为什么呢？"));
					content_list.Add(new DialogNode("这个答案，在十年后我才找到了答案。"));
					content_list.Add(new DialogNode("那是因为我生命的长度被延长了，但是宽度却被我自己压缩了。"));
					content_list.Add(new DialogNode("因为恶疾被治愈，我开始挥霍自己的生命，而后染上了毒瘾，当我回过神的时候，已经是在监狱里等待执行死刑的时候了。"));
					content_list.Add(new DialogNode("我不禁会回想那一次的遭遇，或许让我知道自己生命的期限后，我会更加珍惜地度过，而不是碌碌无为地挥霍生命。"));
					content_list.Add(new DialogNode("生命的长度被延长之后，宽度真的会随之增加吗？"));
					m_end = true;
				}
				else if(m_scene_history.Contains((int)SCENE_TYPE.HOME))
				{
					Singleton<AudioManager>.Instance.PlayMusic(MUSIC_TYPE.father);

					m_status = (int)SCENE_STATE.AFTER_ACTION;
					content_list.Add(new DialogNode("为了缓解医院里那股让我不自在的气氛，我想到赶紧去找一个我认识的医生——孙医生聊天。"));
					content_list.Add(new DialogNode("刚走进孙医生的诊室，看见他在脸色凝重地和一个病人谈话。我轻轻地敲了敲门。"));
					content_list.Add(new DialogNode("“孙医生……”那个病人听到我的声音，猛地回过头来，看见我的脸，又赶紧转过头去。"));
					content_list.Add(new DialogNode("但是我还是看清楚了"));
					content_list.Add(new DialogNode("——那是我的父亲。"));
					content_list.Add(new DialogNode("“……爸？”"));
					content_list.Add(new DialogNode("“你们原来……”"));
					content_list.Add(new DialogNode("孙医生一下愣住了。我追问他发生了什么，他刚开始还支支吾吾，后来被我逼得没办法，终于向我说明了事情的来龙去脉。"));
					content_list.Add(new DialogNode("“有天干活的时候，突然难受得很，刚开始也没当回事，就想着小问题，靠着我这硬棒的身体顶过去。"));
					content_list.Add(new DialogNode("谁知道越顶越难受，还是去了诊所。结果那医生摇摇头，说我诊不出来，你上市里吧。"));
					content_list.Add(new DialogNode("我才开始害怕，要上市里，那看来不是小病了。"));
					content_list.Add(new DialogNode("我就找了一天，瞒着你和你妈，跑了过来，遇见了孙医生。"));
					content_list.Add(new DialogNode("孙医生可是个大好人了，他刚开始还不肯告诉我实情，我就说我能顶得住，您说，没关系。他才告诉我我得的是绝症，不能治，但可以给我续点时间。"));
					content_list.Add(new DialogNode("说出来也不怕你笑话，爸吓得差点晕过去。"));
					content_list.Add(new DialogNode("孙医生就说，你每个月找两天来，我给你开药。这件事也挺久了，还是要感谢孙医生啊。”"));
					content_list.Add(new DialogNode("“爸，其实我……也得了绝症，而且不能续时间，我还剩下几天了。”"));
					content_list.Add(new DialogNode("父亲果然十分惊讶，但很快又恢复了平静。"));
					content_list.Add(new DialogNode("“可能这是天意吧，要我们父子俩，黄泉路上有个伴。”"));
					content_list.Add(new DialogNode("我扶着父亲站起来。“爸，我们出去聊，不要挡着孙医生给别人看病。"));
					content_list.Add(new DialogNode("说起来，孙医生，我们还真是有缘啊，我们两父子要好好感谢您。”"));
					content_list.Add(new DialogNode("“别别别，我受不起，你们两个人的病我都治不好，还感谢我。”"));
					content_list.Add(new DialogNode("“不怪您。”说完，父亲拉着我给孙医生鞠了一躬。"));
					content_list.Add(new DialogNode("我们两个人走出了医院，找了个地方，谈了一下午的心。"));
					content_list.Add(new DialogNode("也许这就是命运的安排吧，跟父亲谈了一下午，我的心情都开朗了许多。"));
					content_list.Add(new DialogNode("我答应父亲帮他保密，然后就分开了，说不定这是最后一次两个人活着分离。"));
					m_diary_builder.Append("父亲也和我一样得了严重的不治之症，然而他却一直都隐瞒着我们，每天带着微笑地生活下去，不让我们担心。相比之下我算是个什么东西，居然如此自怨自艾，不行，至少在最后的时间里，我要好好地陪伴他们。");

				}
				else
				{
					Singleton<AudioManager>.Instance.PlayMusic(MUSIC_TYPE.hospital);
					content_list.Add(new DialogNode("医院。这个我最熟悉的地方，我曾无数次满怀希望地来，而又充满失望地走。"));
					content_list.Add(new DialogNode("而最后我终于解脱了，因为我再也不用来这里了，我的生命已经进入了倒计时。"));
					content_list.Add(new DialogNode("再也不用闻到这里刺鼻的消毒水气味，不用看到一片惨白的床单，不用被压抑的气氛影响，这么说来还是一件好事呢。"));
					
				}
				Singleton<DialogManager>.Instance.ShowDialog(content_list);
				break;
		}
		m_cur_scene = scene;
	}
	public void ChooseAction()
	{
		List<string> choose_list = new List<string>();
		switch (m_cur_scene)
		{
			case (int)SCENE_TYPE.MONTAIN:
				choose_list.Add("调查");
				choose_list.Add("捣乱");
				choose_list.Add("协助");
				choose_list.Add("朝拜");
				Messenger.Broadcast<List<string>>("choose_show", choose_list);
				break;
			case (int)SCENE_TYPE.POLE:
				choose_list.Add("调查");
				choose_list.Add("捣乱");
				choose_list.Add("协助");
				choose_list.Add("挖掘");
				Messenger.Broadcast<List<string>>("choose_show", choose_list);
				break;
			case (int)SCENE_TYPE.DACE_HALL:
				choose_list.Add("调查");
				choose_list.Add("捣乱");
				choose_list.Add("协助");
				choose_list.Add("唱歌");
				Messenger.Broadcast<List<string>>("choose_show", choose_list);
				break;
			case (int)SCENE_TYPE.LAB:
				choose_list.Add("调查");
				choose_list.Add("捣乱");
				choose_list.Add("协助");
				choose_list.Add("制造混乱");
				Messenger.Broadcast<List<string>>("choose_show", choose_list);
				break;
			case (int)SCENE_TYPE.HOME:
				choose_list.Add("调查");
				choose_list.Add("捣乱");
				choose_list.Add("协助");
				choose_list.Add("陪伴");
				Messenger.Broadcast<List<string>>("choose_show", choose_list);
				break;
			case (int)SCENE_TYPE.HOSPITAL:
				choose_list.Add("调查");
				choose_list.Add("捣乱");
				choose_list.Add("协助");
				choose_list.Add("深入调查");
				Messenger.Broadcast<List<string>>("choose_show", choose_list);
				break;
			case (int)SCENE_TYPE.START:
				Messenger.Broadcast("dialog_hide");
				Messenger.Broadcast("calender_show");
				break;
		}
	}

	public void ActionChosen(int chosen)
	{
		m_status = (int)SCENE_STATE.AFTER_ACTION;
		List<DialogNode> content_list = new List<DialogNode>();

		m_diary_builder.Append("\n12月" + (m_cur_day + 1).ToString() + "日       阴\n");

		switch (m_cur_scene)
		{
			case (int)SCENE_TYPE.MONTAIN:
				switch(chosen)
				{
					case 0:
						content_list.Add(new DialogNode("我探索了一下这片神秘的地域，发现了这片地区似乎有人类活动的痕迹，遗留有一些脚印。"));
						content_list.Add(new DialogNode("远处还有一些黑点像建筑一样的物体。没想到这个地方居然也被人类侵占了，真是让人沮丧。"));
						--m_score;
						m_diary_builder.Append("没想到这样的地方都被人类污染了。");
						m_diary_builder.Append(GetScoreDescription(-1));

						Singleton<DialogManager>.Instance.ShowDialog(content_list);						
						break;
					case 1:
						content_list.Add(new DialogNode("来到这个人迹罕至地方，我突发奇想，要是我在这里做点坏事会怎样——反正我要死了——我还没有尝试过这种感觉呢。"));
						content_list.Add(new DialogNode("折断树枝，撕扯草地，我用周围我能捕捉到的一切发泄着我心中的负能量，看着那些被我破坏的东西，心中有一种畸形的快感在迅速地增长。"));
						content_list.Add(new DialogNode("原来做坏事是这么开心的感觉。最后精疲力尽的我倒在地上气喘吁吁，然而我的心情却是无比舒畅的。"));
						++m_score;
						m_diary_builder.Append("做坏事的感觉真好。");
						m_diary_builder.Append(GetScoreDescription(1));

						Singleton<DialogManager>.Instance.ShowDialog(content_list);
						break;
					case 2:
						content_list.Add(new DialogNode("在这片生机勃勃的丛林里，我不禁想着我能为它做些什么，来回报它对我的启迪。"));
						content_list.Add(new DialogNode("虽然我的力量对于大山来说微不足道，然而这是我表达心意的一种方式。"));
						content_list.Add(new DialogNode("我走入林中，捡拾起不知何时丢弃在这里的，已经有些腐坏的垃圾，这些人造的东西不应该出现在这里。"));
						content_list.Add(new DialogNode("虽然来的人不多，但是我最后还是捡了满满的垃圾。"));
						content_list.Add(new DialogNode("离开的时候我听见风吹动树林的沙沙声，似乎是对我维持这片纯净的回应，一种畅快的感觉荡漾在我心中。"));
						++m_score;
						m_diary_builder.Append("清理了垃圾，感觉我为自然又贡献了一份力量。");
						m_diary_builder.Append(GetScoreDescription(1));
						Singleton<DialogManager>.Instance.ShowDialog(content_list);
						break;
					case 3:
						content_list.Add(new DialogNode("我走进树林深处，一株参天巨木赫然进入我的视线。果然，在这种清幽宁静的地方，才能有如此的奇景。"));
						content_list.Add(new DialogNode("我被灵魂深处的一股力量驱使，双膝下跪对着这株神木朝拜。等我站起来，准备转身离开时，发现我已经被一群当地居民包围了。"));
						content_list.Add(new DialogNode("有两个人上前，把我架回了村落里。他们用敬畏的眼神看着我，上前给我松绑，又请我坐到一个豪华的座位上。"));
						content_list.Add(new DialogNode("我突然想到，说不定他们的医术跟现代治疗结合能治好我的病。"));
						content_list.Add(new DialogNode("我找到一个巫医装扮的居民，连说带比划了好一会，他终于明白我是想跟他学巫术。"));
						content_list.Add(new DialogNode("虽然他好像有点奇怪为什么神连巫术都不会，但他还是在接下来的几天里，教给了我巫术的大概，我的心中又燃起一线希望。"));
						m_score += 2;
						m_diary_builder.Append("人迹罕至的山里居然也有村落，还有失传的巫术，真是让人惊讶。");
						m_diary_builder.Append(GetScoreDescription(2));
						Singleton<DialogManager>.Instance.ShowDialog(content_list);
						break;
				}
				break;
			case (int)SCENE_TYPE.POLE:
				switch (chosen)
				{
					case 0:
						content_list.Add(new DialogNode("白雪皑皑的地域，没有什么值得研究的地方。"));
						content_list.Add(new DialogNode("远处似乎有一些黑点一样的东西，但是距离太远没有办法看清楚。"));
						content_list.Add(new DialogNode("地上倒是有一些奇怪动物的毛发，也不知道是什么动物掉落的，我随手捡起来放到了口袋里。"));						
						Singleton<DialogManager>.Instance.ShowDialog(content_list);
						--m_score;
						m_diary_builder.Append("得到了奇怪的标本，应该去研究所看看。");
						m_diary_builder.Append(GetScoreDescription(-1));
						break;
					case 1:
						content_list.Add(new DialogNode("正想把这冰层破坏一番时，我的目光突然被脚下的一片黑影吸引了。"));
						content_list.Add(new DialogNode("随手拿出工具挖掘起来，很快黑影就露出了真面目，原来是一具奇怪的骨骸。"));
						content_list.Add(new DialogNode("这具骨骸体积庞大，结构奇特，判断不出是什么动物的，不过我倒是随手拿了一个。"));
						content_list.Add(new DialogNode("或许可以找机会让人看看到底是什么，我也对这个挺有兴趣。"));
						Singleton<DialogManager>.Instance.ShowDialog(content_list);
						++m_score;
						m_diary_builder.Append("挖出了巨大的骨骸。");
						m_diary_builder.Append(GetScoreDescription(1));
						break;
					case 2:
						content_list.Add(new DialogNode("就在我沉醉于这一片美丽雪景的时候，突然一团白雪从我旁边窜了过去。"));
						content_list.Add(new DialogNode("定睛一看才发现原来是一只兔子，浑身雪白的毛发，的确是很容易让人以为是一团雪而已。"));
						content_list.Add(new DialogNode("而在兔子的后面则有只狐狸在追它。"));
						content_list.Add(new DialogNode("同情心爆发的我过去把狐狸驱逐开了，而那只兔子看了看我，而后又蹦蹦跳跳地离开了。"));
						content_list.Add(new DialogNode("逃到远处的狐狸，突然发出一声凄厉的惨叫，我没太在意，也许是掉进冰窟窿里了吧。"));
						Singleton<DialogManager>.Instance.ShowDialog(content_list);
						++m_score;
						m_diary_builder.Append("救下了一只兔子。");
						m_diary_builder.Append(GetScoreDescription(1));
						break;
					case 3:
						content_list.Add(new DialogNode("跺了跺脚下厚厚的冰层，我想了解一下下面到底是什么。"));
						content_list.Add(new DialogNode("于是我决定砸开这冰层一探究竟。"));
						content_list.Add(new DialogNode("我找到了一片比较薄的区域使出吃奶的力气砸，很快冰层上出现了裂纹。"));
						content_list.Add(new DialogNode("我还来不及欣喜的时候突然轰隆一声，我整个人脚下的一片冰层都坍塌，"));
						content_list.Add(new DialogNode("瞬间掉入了冰冷刺骨的水中，我挣扎了两下便失去了力气，任由身体沉入水底。"));						
						m_score = -100;
						Singleton<DialogManager>.Instance.ShowDialog(content_list);
						m_diary_builder.Append("（代笔）砸开了冰层，掉下水淹死了");
						break;
				}
				break;
			case (int)SCENE_TYPE.DACE_HALL:
				switch (chosen)
				{
					case 0:
						content_list.Add(new DialogNode("我算不得是个观察力仔细的人，但是如果和一群醉酒的人相比的话，我还是相当有优势的。"));
						content_list.Add(new DialogNode("例如现在这个地方，周围的人都喝醉之后，没有醉的人就是最聪明的一个了，"));
						content_list.Add(new DialogNode("一个男子从我身边匆匆跑过，撞了一下我的肩膀却没有道歉就离开了。"));
						content_list.Add(new DialogNode("“真没礼貌。”我低声嘀咕了一声向大门走去，"));
						content_list.Add(new DialogNode("这种品流复杂的地方，终究还是不适合我。"));
						m_score -= 1;
						Singleton<DialogManager>.Instance.ShowDialog(content_list);
						m_diary_builder.Append("现在的人太没有礼貌了，撞到了人也不会道歉。");
						m_diary_builder.Append(GetScoreDescription(-1));
						break;
					case 1:
						m_score -= 2;						
						content_list.Add(new DialogNode("原本就酒量不好的我，几杯酒灌进肚子里之后，已经不太能控制住我的身体了。"));
						content_list.Add(new DialogNode("例如平时一向遵纪守法的好孩子的我，是绝对不会做出把喝完了的啤酒罐丢到别人头上的事情的。"));
						content_list.Add(new DialogNode("然而今天我不仅做了，还在那人愤怒地回头来寻找凶手时嚣张地对着他竖起中指。"));
						content_list.Add(new DialogNode("“看什么看，有种打我啊.....嗝。”"));
						content_list.Add(new DialogNode("事实证明那个人很有种，他果然过来把我狠狠打了一顿，然后丢出了歌舞厅外面。"));
						content_list.Add(new DialogNode("阴冷的夜风吹拂之下，我离家出走的理智有了一点回归的迹象，"));
						content_list.Add(new DialogNode("感觉身边有人经过，我努力地睁开眼睛想要向周围的人求助，但受伤严重的我难以挪动哪怕一步。"));
						content_list.Add(new DialogNode("最后我还是被扫地的工人报警送去了医院，又回到了这个我最讨厌的地方，"));
						content_list.Add(new DialogNode("我一醒来就要回家，不想在医院呆哪怕多一秒。"));
						Singleton<DialogManager>.Instance.ShowDialog(content_list);
						m_diary_builder.Append("被人打了一顿，差点没命，好疼。");
						m_diary_builder.Append(GetScoreDescription(-2));
						break;
					case 2:
						m_score++;
						content_list.Add(new DialogNode("在这种地方能帮上什么忙呢……"));
						content_list.Add(new DialogNode("一阵嘈杂打断了我的思路，我抬头一看，"));
						content_list.Add(new DialogNode("离我不远的一桌看起来醉醺醺的客人把凳子一摔，揪住他们旁边一个畏畏缩缩的服务员的衣领。"));
						content_list.Add(new DialogNode("我跟在几个保安身后，准备去帮他们一把。"));
						content_list.Add(new DialogNode("保安还没和那桌客人说上一句，就被他们猝不及防地打了一拳。"));
						content_list.Add(new DialogNode("当他们真的打起来，我才发现我根本帮不上什么。"));
						content_list.Add(new DialogNode("打肯定是不够他们打的，我努力回想着之前看过的武打小说，绕到一个醉汉背后，使尽全身的力气，往那个醉汉的后颈打去。"));
						content_list.Add(new DialogNode("他还真的倒下了，对付他的那个保安看了我一眼，我突然有一种熟悉的感觉。"));
						content_list.Add(new DialogNode("保安轻易地放倒了剩下的几个人。"));
						Singleton<DialogManager>.Instance.ShowDialog(content_list);
						m_diary_builder.Append("打了别人一顿，好开心。遇到个很面熟的人，但是想不起来是谁。");
						m_diary_builder.Append(GetScoreDescription(1));
						break;
					case 3:
						m_score += 2;
						content_list.Add(new DialogNode("就在我酒醉半酣，昏昏欲睡的时候，台上突然传来一阵刺耳的噪音，"));
						content_list.Add(new DialogNode("没错，就是噪音。"));
						content_list.Add(new DialogNode("那几个染着一头黄发的小年轻在上面声嘶力竭地呐喊，敲着毫无节奏感的鼓点，歌词也是不知所谓。"));
						content_list.Add(new DialogNode("我实在听不下去了，一拍桌子站了起来。"));
						content_list.Add(new DialogNode("“住口！你们唱的也叫歌吗。”"));
						content_list.Add(new DialogNode("或许是他们没想到居然会有人站起来和他们正面刚，"));
						content_list.Add(new DialogNode("不过我想他们应该多积累这样的经验，因为就他们的唱歌水平而言，以后这种情况不会少。"));
						content_list.Add(new DialogNode("我一把冲上去，从目瞪口呆的主唱手中抢下麦克风，借着酒劲就唱了起来。"));
						content_list.Add(new DialogNode("开始的时候我只是清唱，然而歌声的确是倾诉心声的良法，"));
						content_list.Add(new DialogNode("或许是我的歌声感染了他们，渐渐地有了配乐配合，之后是伴唱，最后曲尽的时候，我居然得到了全场一致的掌声。"));
						content_list.Add(new DialogNode("此时我的酒已经醒了大半，清醒状态下的我是断不敢做出如此疯狂的事情的。"));
						content_list.Add(new DialogNode("于是我忙不迭地和大家道谢，连忙从舞台上下来，却差点和一个中年大叔撞个满怀。"));
						content_list.Add(new DialogNode("我一个劲地道歉，他却没有生气，只是笑着点了点头，一路咳嗽着离开了。"));
						content_list.Add(new DialogNode("之后我连忙离开了歌舞厅，但是心中的激动却是久久难以平复。"));
						content_list.Add(new DialogNode("没想到我居然还有唱歌的才能，或许我应该多看看我还有什么才能没有发掘出来，多去各个地方走走。"));
						Singleton<DialogManager>.Instance.ShowDialog(content_list);
						m_diary_builder.Append("今天在歌舞厅高歌一曲，还比过了那些所谓的歌手。我应该多发掘一下，或许还有很多才能我没有发现吧。");
						m_diary_builder.Append(GetScoreDescription(2));
						break;
				}
				break;
			case (int)SCENE_TYPE.LAB:
				switch (chosen)
				{
					case 0:
						m_score -= 1;
						content_list.Add(new DialogNode("究所的周围警戒严密，还有不少形状奇异的建筑，外墙洁白如雪。"));
						content_list.Add(new DialogNode("走进参观区，有不少穿着白大褂的科学家正在忙碌，"));
						content_list.Add(new DialogNode("桌上凌乱地摆着一大堆我看不懂的器材、笔记、药物和化学品，给人一种压抑的感觉。"));
						Singleton<DialogManager>.Instance.ShowDialog(content_list);
						m_diary_builder.Append("研究所的气氛还是一样让人压抑，真是个不愉快的地方。");
						m_diary_builder.Append(GetScoreDescription(-1));
						break;
					case 1:
						m_score += 2;
						content_list.Add(new DialogNode("趁保安不注意，溜进了一间关着门的实验室。"));
						content_list.Add(new DialogNode("把里面的一堆器材和装着五颜六色液体的试管肆意地乱砸一通，"));
						content_list.Add(new DialogNode("又把玻璃渣和笔记一股脑扫到地板上，看着一片狼藉的景象，满意地走出了实验室。"));
						content_list.Add(new DialogNode("既然我不能享受，那你们干脆就不要做下去了。"));
						Singleton<DialogManager>.Instance.ShowDialog(content_list);
						m_diary_builder.Append("做坏事的感觉真好，我也已经不在乎法律的制裁了。谁会去在意一个快要死的人呢。");
						m_diary_builder.Append(GetScoreDescription(2));
						break;
					case 2:
						m_score += 1;
						content_list.Add(new DialogNode("我跟其中一个科学家提出要当志愿者的想法。他马上就同意了，还说正缺人手，然后就让我去打杂。"));
						content_list.Add(new DialogNode("我愣了一秒，然后又想到，也确实是，哪有让志愿者做实验的。"));
						content_list.Add(new DialogNode("接下来的一天，我几乎一刻也没有闲着，在几个实验室之间跑来跑去，送这送那，就差端茶倒水了。"));
						content_list.Add(new DialogNode("不过看着他们接到我递过去的笔记时恍然大悟的样子，我就也不抱怨太多了。"));
						Singleton<DialogManager>.Instance.ShowDialog(content_list);
						m_diary_builder.Append("今天当了一回志愿者，收获良多，最后再给世界做一点贡献吧。");
						m_diary_builder.Append(GetScoreDescription(1));
						break;
					case 3:
						m_score += 2;
						content_list.Add(new DialogNode("看见这些人忙碌的样子我心中的不忿就愈加的深刻。"));
						content_list.Add(new DialogNode("这些人辛辛苦苦地工作，却完全不是为了广大人民谋福利，而是为了那些权贵们的生命。"));
						content_list.Add(new DialogNode("想到这里我决定给他们制造一点小麻烦。"));
						content_list.Add(new DialogNode("来到实验室，非常幸运这里居然空无一人，找到控制台，一把按下红色按钮，我脸上露出了一丝狞笑。"));
						content_list.Add(new DialogNode("走廊上一名普通的研究员走着的时候，突然看见一人脸上挂着难以掩抑的惊慌踉踉跄跄地跑过来。"));
						content_list.Add(new DialogNode("“实验用的动物，全部逃出来了，快走。”"));
						content_list.Add(new DialogNode("那名研究员惊得脸色都变了，然而还是转身拉响了警报，然而想要逃跑的时候却被一只猩猩扑倒在地，惨叫一声便悄无声息。"));
						content_list.Add(new DialogNode("而原先逃跑的那个人却早已逃之夭夭。"));
						content_list.Add(new DialogNode("“这次算是给你们一个小的教训。”"));
						content_list.Add(new DialogNode("加快脚步赶紧逃离这个已经陷入一片混乱的研究所，为了逃命人们把所有的门都打开了"));
						content_list.Add(new DialogNode("因此也得以见到许多平时难得一见的珍奇物品，如果是平时的话，我肯定会好好地研究一下，特别是那块巨大的冰疙瘩。"));
						content_list.Add(new DialogNode("突然一片黑影迎面向我袭来，我扯下一看原来是一件被脱下的研究袍，"));
						content_list.Add(new DialogNode("连衣服也不要了，足见那些人是多么的惊慌失措。一把甩开那件衣服，我混入了混乱的人流之中，顺利地离开了研究所。"));
						Singleton<DialogManager>.Instance.ShowDialog(content_list);
						m_diary_builder.Append("把研究所大闹了一场，看着他们惊慌失措的样子格外的有快意。身体有点不舒服，我还是早点休息吧。");
						m_diary_builder.Append(GetScoreDescription(2));
						break;
				}
				break;
			case (int)SCENE_TYPE.HOME:
				switch (chosen)
				{
					case 0:
						m_score -= 1;
						content_list.Add(new DialogNode("家乡的气候和水土，和城市地区里的完全不一样。"));
						content_list.Add(new DialogNode("家乡这几年变化也很大，看着这熟悉而又陌生的景色，我心旷神怡，又不禁暗自感伤："));
						content_list.Add(new DialogNode("这么美丽的风景，我却不能再欣赏多久。"));
						Singleton<DialogManager>.Instance.ShowDialog(content_list);
						m_diary_builder.Append("今天回到了暌违已久的家乡，然而却是物是人非，许多记忆都变成了回忆，或许不久之后我也会成为回忆，只是不知道多少人会记住我呢。");
						m_diary_builder.Append(GetScoreDescription(-1));
						break;
					case 1:
						m_score -= 1;
						content_list.Add(new DialogNode("我环顾四周，发现群青绿水里突兀地竖立着一根烟囱。"));
						content_list.Add(new DialogNode("在这种景色优美的地方竟然有工厂来污染环境！我找到了下一个破坏的对象。"));
						content_list.Add(new DialogNode("我走进车间，工人们都在专心工作，没有人理我。"));
						content_list.Add(new DialogNode("我找到了工厂的电闸，刚想把电断掉的时候，背后传来了一声呵斥："));
						content_list.Add(new DialogNode("“喂！你想干什么！”"));
						content_list.Add(new DialogNode("我心里一惊，拔腿就跑。后面的人好像也追了上来，边追边喊“站住”。"));
						content_list.Add(new DialogNode("听着越来越近的脚步声，我知道这次肯定要被抓住了。后面的人一扑，我重重.....地摔在地上。乡里人听到喊声，都围过来看热闹。"));
						content_list.Add(new DialogNode("他质问我：“为什么要打电闸？！”"));
						content_list.Add(new DialogNode("我不甘示弱地反击道：“工厂这种东西，是污染环境的，如果继续开下去，这片青山绿水迟早会消失！”"));
						content_list.Add(new DialogNode("那个人叹了一口气，说：“我们也不想的，只是这么多年来发展的经济，全是靠这个工厂撑起来的。我也理解你，就不为难你了，我放你走吧。”"));
						content_list.Add(new DialogNode("我不甘心却又无可奈何地离开了。家乡里的人还是太愚昧了，不能理解到如果这个工厂继续开下去，会有多么可怕的后果！"));
						Singleton<DialogManager>.Instance.ShowDialog(content_list);
						m_diary_builder.Append("没想到家乡的人居然变得如此的短视，这样出卖土地和以环境为代价的利益是不可取得，但是他们都被利益蒙蔽了双眼，我的心好痛。");
						m_diary_builder.Append(GetScoreDescription(-1));
						break;
					case 2:
						m_score += 2;
						content_list.Add(new DialogNode("我走进家里，发现父母都出门了。突然想到，我干脆就帮父母做一次家务吧。"));
						content_list.Add(new DialogNode("说干就干，我把工具都找出来，扫地拖地擦桌洗碗，都做了一遍。"));
						content_list.Add(new DialogNode("终于完成了之后，我一下累倒在沙发上。看了一眼钟，已经过了两个半小时。"));
						content_list.Add(new DialogNode("我才终于理解到父母平时多辛苦，我却忙于工作，很少回来陪他们，帮他们的忙。"));
						content_list.Add(new DialogNode("门“嘎吱”一声打开了，我循声望去，原来是母亲回来了。她看到家里一片整洁，露出了疑惑的目光。"));
						content_list.Add(new DialogNode("当她看到我的时候，激动地说不出话来，眼里闪着泪光，我不管劳累的身体，赶紧上前，给了母亲一个大大的拥抱。"));
						content_list.Add(new DialogNode("对了，为什么父亲没有回来呢？"));
						Singleton<DialogManager>.Instance.ShowDialog(content_list);
						m_diary_builder.Append("今天在家里操持了一天的家务，发现比想象中的要累得多，看来我应该多多体谅母亲的辛苦。看到她的白发时我才真切地认识到，母亲老了。");
						m_diary_builder.Append(GetScoreDescription(2));
						break;
					case 3:
						m_score++;
						content_list.Add(new DialogNode("我回到家里，发现没有人。倒正好给了我一个准备惊喜的好机会。"));
						content_list.Add(new DialogNode("我走进厨房，开始准备做饭，心里还想着父母看到了会有多开心。"));
						content_list.Add(new DialogNode("虽然我的厨艺不算非常好，但做一餐晚饭还是绰绰有余的。"));
						content_list.Add(new DialogNode("我把一盘又一盘菜端到饭桌上，刚好做完最后一道菜时，父母回来了。我赶紧招呼："));
						content_list.Add(new DialogNode("“爸妈，你们回来得正好，赶紧吃饭吧，我给你们做了一桌菜。”"));
						content_list.Add(new DialogNode("父母赶紧坐下来，我给他们一人盛了一碗汤，看着他们脸上洋溢着幸福的表情，我内心却万分愧疚。"));
						content_list.Add(new DialogNode("想到自己时日不多，不能再陪他们，我偷偷擦掉了眼角的泪水。"));
						Singleton<DialogManager>.Instance.ShowDialog(content_list);
						m_diary_builder.Append("没想到只是一次简单的陪伴居然就让父母如此的开心，看来真是陪伴得他们太少了，然而我现在陪伴他们的时间却是已经所剩无几了。");
						m_diary_builder.Append(GetScoreDescription(1));
						break;
				}
				break;
			case (int)SCENE_TYPE.HOSPITAL:
				switch (chosen)
				{
					case 0:
						m_score--;
						content_list.Add(new DialogNode("这个充斥着消毒水气味和我噩梦般的回忆的地方，我实在没有什么动力去多看两眼。"));
						content_list.Add(new DialogNode("急诊里似乎有一个严重受伤的病人，他不时发出的痛苦的呻吟贯彻我的耳朵。"));
						content_list.Add(new DialogNode("我摇摇头，让自己不要想这么多。"));
						content_list.Add(new DialogNode("我看到有穿着研究所衣服的人走过，好奇地跟了上去，"));
						content_list.Add(new DialogNode("但我太入神以至于没有看到迎面而来的一个女子，不小心撞了上去，连忙说了两声“抱歉。”"));
						Singleton<DialogManager>.Instance.ShowDialog(content_list);
						m_diary_builder.Append("回到这个我熟悉又厌恶的地方，依然是不时能听到惨叫声和见到行色匆匆的人，真是讨厌这个地方。");
						m_diary_builder.Append(GetScoreDescription(-1));
						break;
					case 1:
						m_score++;
						content_list.Add(new DialogNode("找到了曾经的主治医生的诊室，一脚把门踹开，"));
						content_list.Add(new DialogNode("趁他没反应过来，把他桌上的电脑和诊治工具全都砸到地上，又打了他一顿，"));
						content_list.Add(new DialogNode("然后在离开的时候顺手把走廊上护士的推车撞翻了。我总算是把这个心结发泄了出来，长舒了一口气。"));
						Singleton<DialogManager>.Instance.ShowDialog(content_list);
						m_diary_builder.Append("把以前的那个主治医生打了一顿，出了心头的一股恶气。");
						m_diary_builder.Append(GetScoreDescription(1));
						break;
					case 2:
						m_score++;
						content_list.Add(new DialogNode("来到医院里，刚进门就偶遇了孙医生，"));
						content_list.Add(new DialogNode("虽然他不是我的主治医生，但是当初也是尽心尽力地治疗我的，所以我还是很感激他的。"));
						content_list.Add(new DialogNode("他热情地邀请我到他的诊室坐坐，我也没有拒绝。他一边接治病人，一边和我热情地谈话。"));
						content_list.Add(new DialogNode("突然，孙医生桌上的电话响了，他接起来，应了几声，对我说：“不好意思啊，我有点事要先离开一下，你在这里等等我。”"));
						content_list.Add(new DialogNode("我忙点头。看着孙医生门前的一堆焦急的病人，我心中涌起了帮帮孙医生的念头。"));
						content_list.Add(new DialogNode("久病成医，我也自学了不少医学方面的知识，我对自己还是有信心的，就坐到了孙医生的位子上，帮他接诊病人。"));
						content_list.Add(new DialogNode("刚诊断完第一个病人，孙医生就回来了，"));
						content_list.Add(new DialogNode("他看到这个情景，激动地走过来拉着我的手，说：“真是太感谢你了，每天都有很多病人找我，我都忙不过来了。”"));
						content_list.Add(new DialogNode("我赶紧找了个理由离开了，不然估计会被孙医生留下来干一天的活。"));
						Singleton<DialogManager>.Instance.ShowDialog(content_list);
						m_diary_builder.Append("遇到了以前相当照顾我的医生，还帮他诊治了一个病人，看来还真是久病成医呢。");
						m_diary_builder.Append(GetScoreDescription(1));
						break;
					case 3:
						m_score -= 2;
						content_list.Add(new DialogNode("好奇心驱使着我跟着那些研究员，他们进入了太平间，我有点惊讶："));
						content_list.Add(new DialogNode("为什么研究员好端端的要来医院的太平间？"));
						content_list.Add(new DialogNode("我没有勇气跟进去，就躲在门旁，从门缝偷偷看着。"));
						content_list.Add(new DialogNode("医生拉开一个柜子，把里面放着的一具尸体放在一个大袋子里。来来回回一共有十多具。"));
						content_list.Add(new DialogNode("研究员把袋子的拉链拉上，然后拿出一大叠钞票，递给那个医生。"));
						content_list.Add(new DialogNode("医院和研究所在做尸体交易！"));
						content_list.Add(new DialogNode("我被吓得倒吸一口冷气，赶紧离开了那个地方，却又一直在思考，研究所要尸体做什么呢？"));
						Singleton<DialogManager>.Instance.ShowDialog(content_list);
						m_diary_builder.Append("我居然发现了医院和研究所之间不可告人的秘密，哼哼，我要去报警，我要警察查明真相。");
						m_diary_builder.Append(GetScoreDescription(-2));
						break;
				}
				break;
			case (int)SCENE_TYPE.START:
				break;
		}
		m_diary_builder.Append("\n\n");
		m_action_history.Add(chosen);
	}

	public void EndDialog()
	{
		m_scene_history.Add(m_cur_scene);

		if(m_end)
		{
			Messenger.Broadcast("dialog_hide");
			Messenger.Broadcast("ending_show");
			HideScene();
			return;
		}

		switch (m_cur_scene)
		{
			case (int)SCENE_TYPE.MONTAIN:
			case (int)SCENE_TYPE.POLE:
			case (int)SCENE_TYPE.DACE_HALL:
			case (int)SCENE_TYPE.LAB:
			case (int)SCENE_TYPE.HOME:
			case (int)SCENE_TYPE.HOSPITAL:
				if (m_status == (int)SCENE_STATE.BEFORE_ACTION)
				{
					ChooseAction();
				}
				else
				{
					++m_cur_day;
					SetScene((int)SCENE_TYPE.START);
				}
				break;
			case (int)SCENE_TYPE.START:
				if (m_cur_day < TOTAL_DAY)
				{
					Messenger.Broadcast("dialog_hide");
					Messenger.Broadcast("calender_show");
				}
				else
				{
					Messenger.Broadcast("dialog_hide");
					Messenger.Broadcast("ending_show");
				}
				break;
		}


	}

	public int GetSceneTime(int type)
	{
		int time = 0;
		for(int i = 0; i < m_scene_history.Count; ++i)
		{
			if (m_scene_history[i] == type)
			{
				++time;
			}
		}
		return time;
	}
	
	public void HideScene()
	{
		for (int i = 0; i < transform.childCount; ++i)
		{
			transform.GetChild(i).gameObject.SetActive(false);
		}
	}
	public void SetSceneState(int scene, int state)
	{

	}	
	public void CloseCalender(int scene)
	{
		SetScene(scene);
	}
	public void NextScene()
	{
	}	

	string GetScoreDescription(int value)
	{
		string result = "";
		switch(value)
		{
			case 1:
				result = "\n心情变好了。";
				break;
			case 2:
				result = "\n今天过得好开心。";
				break;
			case 3:
				result = "\n天大的惊喜，今天我的人生是彩色的。";
				break;
			case -1:
				result = "\n不开心。";
				break;
			case -2:
				result = "\n真是让人沮丧。";
				break;
			case -3:
				result = "\n真是让人沮丧。";
				break;
		}
		return result;
	}
}